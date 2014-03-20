using UnityEngine;
using System.Collections;

public class ladderController : MonoBehaviour
{
		public int m_ladderSpeed;
		public float ladderCheckScale;
		public Transform m_middleRight;
		public Transform m_middleLeft;
		private bool m_hitLadder = false;
		private bool m_hitLastLadder;
		// Use this for initialization
		public bool getHitLadder ()
		{
				return m_hitLastLadder;
		}
		void ladderNavigate ()
		{
				float vertDir = Input.GetAxis ("Vertical");
				Vector3 amountMove = (new Vector3 (0, vertDir, 0) * m_ladderSpeed * Time.fixedDeltaTime);
				Collider2D[] coll = new Collider2D[1];
				float scale = ladderCheckScale;
				int onlyLadders = 1 << 8;
				int numHit = Physics2D.OverlapCircleNonAlloc ((Vector2)transform.position, scale + .1f, coll, onlyLadders);
				bool m_hitLadder = false;
				if (numHit > 0) {
						m_hitLadder = (coll [0].CompareTag ("Ladder"));
						//Debug.Log (m_hitLadder);
						if (m_hitLadder) {
								/*transform.position += amountMove;*/
								CheckForCol (vertDir, onlyLadders, amountMove);
						}	
				}
				m_hitLastLadder = m_hitLadder;
		}	
	
		void CheckForCol (float vertDir, int onlyLadders, Vector3 amountMove)
		{
				RaycastHit2D vertInfoLeft;
				RaycastHit2D vertInfoRight;
				
				int allButLadders = ~onlyLadders;
		
				float checkSize = 1F;
				float scale = transform.localScale.x / 2f;
				
				Vector2 rightPos = m_middleLeft.position;
				Vector2 leftPos = m_middleRight.position;
				
				float yPos = rightPos.y;
				float xPosLeft = leftPos.x;
				float xPosRight = rightPos.x;
				

				float dir = Mathf.Sign (vertDir);
/*				if (vertDir == 0) 
						dir = 0;*/
				
				float offset = scale * dir;
				Debug.DrawLine (new Vector3 (xPosLeft, yPos + offset, 0), new Vector3 (xPosLeft, yPos + (checkSize * dir) + offset, 0), Color.red);
				Debug.DrawLine (new Vector3 (xPosRight, yPos + offset, 0), new Vector3 (xPosRight, yPos + (checkSize * dir) + offset, 0), Color.blue);
				
				Vector2 direction = Vector2.up * dir;
				direction.Normalize ();
				//Debug.Log ("size of raycast is:" + direction * checkSize);
				
				vertInfoLeft = Physics2D.Raycast (new Vector2 (xPosLeft, yPos + offset), Vector2.up * dir, checkSize, allButLadders);
				vertInfoRight = Physics2D.Raycast (new Vector2 (xPosRight, yPos + offset), Vector2.up * dir, checkSize, allButLadders);
				
				float leftDiff = Mathf.Abs (yPos + offset - vertInfoLeft.point.y);
				float rightDiff = Mathf.Abs (yPos + offset - vertInfoRight.point.y);
				float leastDiff;
				
/*				if (vertInfoLeft.fraction == 0) {
						Debug.Log ("lpoint: " + vertInfoLeft.point.y);
						//leftDiff = Mathf.Infinity;
				}
				if (vertInfoRight.fraction == 0) {
						Debug.Log ("rpoint: " + vertInfoRight.point.y);
/*						rightDiff = Mathf.Infinity;
				}*/
				
				if (leftDiff < rightDiff) {
						leastDiff = leftDiff;
				} else {
						leastDiff = rightDiff;
				}
				bool collidersNull = vertInfoLeft.collider == null && vertInfoRight.collider == null;
				//Debug.Log ("Yposandoffset: " + (yPos + offset) + " vert: " + vertInfoLeft.point.y + " frac: " + vertInfoLeft.fraction + " verDir: " + vertDir + " Least: " + leastDiff);
				//Debug.Log ("Left: " + leftDiff + " Right: " + rightDiff + " Least: " + leastDiff);
				//Debug.Log ("leastDiff: " + leastDiff + " amountMove.y: " + Mathf.Abs (amountMove.y));
				//Debug.Log ("test: " + Time.fixedDeltaTime * m_ladderSpeed * vertDir + " ammov: " + amountMove.y);
				//Debug.Log ("lfrac: " + vertInfoLeft.fraction + " rfrac: " + vertInfoRight.fraction);
				if ((leastDiff <= Mathf.Abs (amountMove.y)) && (!collidersNull)) {
						transform.position += (new Vector3 (0, dir * leastDiff, 0));
						//Debug.Log ("constrain, col:" + collidersNull);
				} else if ((leastDiff > Mathf.Abs (amountMove.y)) || (collidersNull)) {
						transform.position += amountMove;
						//Debug.Log ("free, col:" + collidersNull);
				}
/*				if (vertInfoLeft.collider != null)
						Debug.Log (vertInfoLeft.collider.gameObject.name);*/
				
		}
		void Update ()
		{
				ladderNavigate ();
		}
}
