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
		
		bool clear (Vector2 checkSpot)
		{
				int onlyPlayer = 1 << 9;
				int allButPlayer = ~onlyPlayer;
				Collider2D[] hit = new Collider2D[1];
				Vector3 pos = transform.position;
				Physics2D.OverlapPointNonAlloc (checkSpot, hit, allButPlayer);
				if (hit [0] != null) {
						return hit [0].CompareTag ("Ladder") || hit [0].CompareTag ("PickUp");
				} else
						return true;
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
				if (clear (downSpace) && (!ladderPresent (pos)) && (!ladderPresent (downSpace))) {
						m_falling = true;
						transform.position -= new Vector3 (0, 1, 0);
				} else {
						m_falling = false;
				}
				
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
				
				if (((up) && (onLadder)) && clear (upSpace)) {
						transform.position += new Vector3 (0, 1, 0);
				} else if (((down) && (onLadder || ladderBelow)) && clear (downSpace)) {
						transform.position += new Vector3 (0, -1, 0);
				} else if ((left) && clear (leftSpace)) {
						transform.position += new Vector3 (-1, 0, 0);
				} else if ((right) && clear (rightSpace)) {
						transform.position += new Vector3 (1, 0, 0);
				} 
		}
}
