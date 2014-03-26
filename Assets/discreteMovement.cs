using UnityEngine;
using System.Collections;

public class discreteMovement : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				bool up = Input.GetKey ("w");
				bool down = Input.GetKey ("s");
				bool left = Input.GetKey ("a");
				bool right = Input.GetKey ("d");
				makeMove (up, down, left, right);
		}
		Coroutine makeMoveTime ()
		{
			
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
		bool ladderPresent ()
		{
				int onlyPlayer = 1 << 9;
				int allButPlayer = ~onlyPlayer;
				Collider2D[] hit = new Collider2D[1];
				Vector3 pos = transform.position;
				Physics2D.OverlapPointNonAlloc (new Vector2 (pos.x, pos.y), hit, allButPlayer);
				if (hit [0] != null) {
						return hit [0].CompareTag ("Ladder");
				} else {
						return false;
				}
		}
		
		void makeMove (bool up, bool down, bool left, bool right)
		{
				Vector2 pos = transform.position;
				Vector2 upSpace = new Vector2 (pos.x, pos.y + 1);
				Vector2 downSpace = new Vector2 (pos.x, pos.y - 1);
				Vector2 leftSpace = new Vector2 (pos.x - 1, pos.y);
				Vector2 rightSpace = new Vector2 (pos.x + 1, pos.y);
				bool onLadder = ladderPresent ();
				
				if (((up) && (onLadder)) && clear (upSpace)) {
						transform.position += new Vector3 (0, 1, 0);
				} else if (((down) && (onLadder)) && clear (downSpace)) {
						transform.position += new Vector3 (0, -1, 0);
				} else if ((left) && clear (leftSpace)) {
						transform.position += new Vector3 (-1, 0, 0);
				} else if ((right) && clear (rightSpace)) {
						transform.position += new Vector3 (1, 0, 0);
				} 
		}
}
