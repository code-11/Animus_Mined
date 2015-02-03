using UnityEngine;
using System.Collections;

public class fallController : MonoBehaviour
{
		public Transform m_leftBottom;
		public Transform m_rightBottom;
		public float m_gravity;
		public float m_timeToFall;
		public bool m_waitToFall;
		public bool m_killOnHit;
		
		void GravityWithWait ()
		{
				StartCoroutine (GravityTimer ());
/*				if (m_curTimeToFall <= 0) {
						Gravity ();
				} else {
						m_curTimeToFall -= 1 * Time.deltaTime;
				}*/
		}
		bool checkForLadder ()
		{
				ladderController ladderScript = gameObject.GetComponent<ladderController> ();
				bool ladderPresent = false;
/*				Debug.Log (ladderScript == null);*/
				if (ladderScript != null) {
						ladderPresent = ladderScript.getHitLadder ();
				}
				//Debug.Log (ladderPresent);
				return ladderPresent;
		}
		IEnumerator GravityTimer ()
		{
				yield return new WaitForSeconds (m_timeToFall);
				Gravity ();
		}
		void Gravity ()
		{
				RaycastHit2D downInfoLeft;
				RaycastHit2D downInfoRight;
				RaycastHit2D downInfoMiddle;
		
				float gravSize = 1F;
		
				Vector2 leftBottom = m_leftBottom.position;
				Vector2 rightBottom = m_rightBottom.position;
		
				float yPos = leftBottom.y;
				float xPosLeft = leftBottom.x;
				float xPosRight = rightBottom.x;
		
				//Debug.DrawLine (new Vector3 (xPosLeft, yPos, 0), new Vector3 (xPosLeft, yPos - gravSize, 0), Color.red);
				//Debug.DrawLine (new Vector3 (xPosRight, yPos, 0), new Vector3 (xPosRight, yPos - gravSize, 0), Color.blue);
		
				downInfoLeft = Physics2D.Raycast (new Vector2 (xPosLeft, yPos), -Vector2.up, gravSize);
				downInfoRight = Physics2D.Raycast (new Vector2 (xPosRight, yPos), -Vector2.up, gravSize);
				downInfoMiddle = Physics2D.Raycast (new Vector2 (transform.position.x, yPos), -Vector2.up, gravSize / 2);
		
				float leftDiff = Mathf.Abs (yPos - downInfoLeft.point.y);
				float rightDiff = Mathf.Abs (yPos - downInfoRight.point.y);
				float leastDiff;
		
				if (leftDiff < rightDiff) {
						leastDiff = leftDiff;
				} else {
						leastDiff = rightDiff;
				}
				Vector3 amountMove = (new Vector3 (0, m_gravity, 0)) * Time.fixedDeltaTime;
		
				bool collidersNull = downInfoLeft.collider == null && downInfoRight.collider == null;
				if ((leastDiff <= -amountMove.y) /*|| (!collidersNull)*/) {
						transform.position += (new Vector3 (0, -leastDiff, 0));
				} else if ((collidersNull) || (leastDiff >= -amountMove.y)) {
						transform.position += amountMove;
				}	
				if (m_killOnHit) {
						CheckPlayerHit (downInfoLeft, downInfoRight, leftDiff, rightDiff);
				}
				if (downInfoMiddle.collider != null) {
						GameObject hitObj = downInfoMiddle.collider.gameObject;
						if (hitObj.CompareTag ("PickUp")) {
								Destroy (hitObj);
						}
				}
		}
		void CheckPlayerHit (RaycastHit2D downInfoLeft, RaycastHit2D downInfoRight, float leftDiff, float rightDiff)
		{
				Transform leftHitObj = null;
				Transform rightHitObj = null;
				if (downInfoLeft.collider) {
						leftHitObj = downInfoLeft.collider.transform;
				}
				if (downInfoRight.collider) {
						rightHitObj = downInfoRight.collider.transform;
				}				
			
				if (leftHitObj != null) {
						if ((leftHitObj.CompareTag ("Player")) && (leftDiff < .1)) {		
								leftHitObj.gameObject.SendMessage ("killSelf");
						}
				} else if (rightHitObj != null) {
						if ((rightHitObj.CompareTag ("Player")) && (rightDiff < .1)) {
								rightHitObj.gameObject.SendMessage ("killSelf");
						}
				}
		}
		// Update is called once per frame
		void Update ()
		{
				if (m_waitToFall) {
						GravityWithWait ();
				} else {
						if (!checkForLadder ()) {
								Gravity ();
						}
				}

		}
}
