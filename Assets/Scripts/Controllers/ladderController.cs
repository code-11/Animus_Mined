using UnityEngine;
using System.Collections;

public class ladderController : MonoBehaviour
{
		public int m_ladderSpeed;
		public float ladderCheckScale;
		public Transform m_middleRight;
		public Transform m_middleLeft;
		// Use this for initialization
		void ladderNavigate ()
		{
				float vertDir = Input.GetAxis ("Vertical");
				Vector3 amountMove = (new Vector3 (0, vertDir) * m_ladderSpeed * Time.fixedDeltaTime);
				Collider2D[] coll = new Collider2D[2];
				float scale = ladderCheckScale;
				int numHit = Physics2D.OverlapCircleNonAlloc ((Vector2)transform.position, scale, coll);
				bool hitLadder = false;
				if ((numHit > 1)) {
						hitLadder = ((coll [1].CompareTag ("Ladder"))) || (coll [0].CompareTag ("Ladder"));
						if (hitLadder)
								transform.position += amountMove;
						CheckForCol (vertDir);
				}
		}	
	
		void CheckForCol (float vertDir)
		{
				RaycastHit2D vertInfoLeft;
				RaycastHit2D vertInfoRight;
		
				float checkSize = 1F;
				float scale = transform.localScale.x / 2f;
				
				Vector2 rightPos = m_middleLeft.position;
				Vector2 leftPos = m_middleRight.position;
				
				float yPos = rightPos.y;
				float xPosLeft = leftPos.x;
				float xPosRight = rightPos.x;
				
				float dir = Mathf.Sign (vertDir);
				float offset = scale * dir;
				Debug.DrawLine (new Vector3 (xPosLeft, yPos + offset, 0), new Vector3 (xPosLeft, yPos + (checkSize * dir) + offset, 0), Color.red);
				Debug.DrawLine (new Vector3 (xPosRight, yPos + offset, 0), new Vector3 (xPosRight, yPos + (checkSize * dir) + offset, 0), Color.blue);
				
				vertInfoLeft = Physics2D.Raycast (new Vector2 (xPosLeft, yPos + offset), Vector2.up * dir, checkSize);
				vertInfoRight = Physics2D.Raycast (new Vector2 (xPosRight, yPos + offset), Vector2.up * dir, checkSize);
				
				Debug.Log (vertInfoLeft.collider.gameObject.name);
				
		}
		void Update ()
		{
				ladderNavigate ();
		}
}
