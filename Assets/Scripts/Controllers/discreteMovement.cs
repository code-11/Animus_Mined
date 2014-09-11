using UnityEngine;
using System.Collections;

public class discreteMovement : MonoBehaviour
{
		
		private bool m_allowMovement;
		private bool m_waiting;
		private bool m_falling;
		private bool m_gravityWaiting;
		private bool m_allowFalling;
		public float m_movementDelay = .01f;
		public float m_gravityDelay = .01f;
		private enum m_dirs
		{
				up,
				down,
				left,
				right}
		;
		private int m_heading = (int)m_dirs.left;
		public Transform spPos;
		
		// Use this for initialization
		void Start ()
		{
				m_allowMovement = true;
				m_waiting = false;
				m_falling = false;
				m_gravityWaiting = false;
				m_allowFalling = true;
		}
	
		// Update is called once per frame
		void Update ()
		{						
				if (m_allowMovement) {
						bool up = Input.GetKey ("w");
						bool down = Input.GetKey ("s");
						bool left = Input.GetKey ("a");
						bool right = Input.GetKey ("d");
						makeMove (up, down, left, right);
						m_allowMovement = false;
				} else if ((!m_waiting) && (!m_falling)) {
						StartCoroutine (makeMoveTime ());
				}
				if (m_allowFalling) {
						gravity ();
						m_allowFalling = false;
				} else if (!m_gravityWaiting) {
						StartCoroutine (gravityTime ());
				}
						
		}
		IEnumerator makeMoveTime ()
		{
				m_waiting = true;
				yield return new WaitForSeconds (m_movementDelay);
				m_allowMovement = true;		
				m_waiting = false;
		}
		IEnumerator gravityTime ()
		{
				m_gravityWaiting = true;
				yield return new WaitForSeconds (m_gravityDelay);
				m_allowFalling = true;
				m_gravityWaiting = false;
				
		}
		
		void changeHeading (int dir)
		{
				if (m_heading == (int)m_dirs.left) {
						m_heading = (int)m_dirs.right;
				} else if (m_heading == (int)m_dirs.right) {
						m_heading = (int)m_dirs.left;
				}
				spPos.Rotate (Vector2.up, 180f);
		}
		
		bool checkPerm (permeability perm, int dir)
		{
				//Directions are flipped becase permeability is from block perspective
				//But enum is from player perspective. 
				//A player moving left enters a block on the right side.
				if (dir == (int)m_dirs.down) {
						return perm.getUp ();
				} else if (dir == (int)m_dirs.up) {
						return perm.getDown ();
				} else if (dir == (int)m_dirs.left) {
						return perm.getRight ();
				} else if (dir == (int)m_dirs.right) {
						return perm.getLeft ();
				} else {
						return false;
				}
		
		}
		
		bool clear (Vector2 checkSpot, int dir)
		{
				int onlyPlayer = 1 << 9;
				int allButPlayer = ~onlyPlayer;
				Collider2D[] hit = new Collider2D[1];
				Physics2D.OverlapPointNonAlloc (checkSpot, hit, allButPlayer);
				if (hit [0] != null) {
						permeability perm = hit [0].gameObject.GetComponent<permeability> ();
						bool permeable = checkPerm (perm, dir);
						return permeable;
				} else {
						craftManager craftMan = gameObject.GetComponent<craftManager> ();
						if (craftMan != null) {
								if (craftMan.getGuiMenuUp ()) {
										return false;
								}
						} else {
								Debug.Log ("Play does not have a craftManager somehow");
						}
						return true;
				}
		}
		bool ladderPresent (Vector2 checkSpot)
		{
				int onlyPlayer = 1 << 9;
				int allButPlayer = ~onlyPlayer;
				Collider2D[] hit = new Collider2D[1];
				Physics2D.OverlapPointNonAlloc (checkSpot, hit, allButPlayer);
				if (hit [0] != null) {
						return hit [0].CompareTag ("Ladder");
				} else {
						return false;
				}
		}
		
		void gravity ()
		{
				Vector2 pos = transform.position;
				Vector2 downSpace = new Vector2 (pos.x, pos.y - 1);
				if (clear (downSpace, (int)m_dirs.down) && (!ladderPresent (pos)) && (!ladderPresent (downSpace))) {
						m_falling = true;
						transform.position -= new Vector3 (0, 1, 0);
				} else {
						m_falling = false;
				}
				
		}
		
		void killSelf ()
		{
				Time.timeScale = 0.3F;
				Destroy (this.gameObject);
				Debug.Log ("Game Over");
		}
	
		void makeMove (bool up, bool down, bool left, bool right)
		{
				Vector2 pos = transform.position;
				Vector2 upSpace = new Vector2 (pos.x, pos.y + 1);
				Vector2 downSpace = new Vector2 (pos.x, pos.y - 1);
				Vector2 leftSpace = new Vector2 (pos.x - 1, pos.y);
				Vector2 rightSpace = new Vector2 (pos.x + 1, pos.y);
				bool onLadder = ladderPresent (new Vector2 (pos.x, pos.y));
				bool ladderBelow = ladderPresent (new Vector2 (pos.x, pos.y - 1));
				
				if (((up) && (onLadder)) && clear (upSpace, (int)m_dirs.up)) {
						transform.position += new Vector3 (0, 1, 0);
				} else if (((down) && (onLadder || ladderBelow)) && clear (downSpace, (int)m_dirs.down)) {
						transform.position += new Vector3 (0, -1, 0);
				} else if ((left) && clear (leftSpace, (int)m_dirs.left)) {
						if (m_heading == (int)m_dirs.left) {
								transform.position += new Vector3 (-1, 0, 0);
						} else {
								changeHeading ((int)m_dirs.left);
						}
				} else if ((right) && clear (rightSpace, (int)m_dirs.right)) {
						if (m_heading == (int)m_dirs.right) {
								transform.position += new Vector3 (1, 0, 0);
						} else {
								changeHeading ((int)m_dirs.right);
						}
				}
		}
}