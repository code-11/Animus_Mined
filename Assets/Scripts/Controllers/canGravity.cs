using UnityEngine;
using System.Collections;

public class canGravity : MonoBehaviour
{
		public Transform m_leftBottom;
		public Transform m_rightBottom;
		public float m_gravitySpeed = -5f;
		public float m_gravityLen = .1f;
		
		//How long between checking if there is something below the block
		public float m_checkTime;
		
		//How long to wait before falling after noticing there isn't something below the block.
		public float m_waitTime;
		
		public bool m_killOnHit;
		public bool m_waiting = false;
		public enum State
		{
				Still,
				Wait,
				Fall
		}
		;
		public State m_curState = State.Still;
		// Update is called once per frame
		void Update ()
		{
				if (!m_waiting) {
						StartCoroutine (stateMachine ());
				}
		}
		IEnumerator stateMachine ()
		{
				if (m_curState == State.Still) {
						if (castMiddle () == null) {
								//if there is nothing below the block
								m_curState = State.Wait;
						}
						m_waiting = true;
						yield return new WaitForSeconds (m_checkTime);
						m_waiting = false;
						//Need to wait here
				} else if (m_curState == State.Wait) {
						m_waiting = true;
						yield return new WaitForSeconds (m_waitTime);
						m_waiting = false;
						m_curState=State.Fall;
				} else if (m_curState == State.Fall) {
						if (gameObject.CompareTag ("Hypersthene")) {
								checkPickupsAndPlayerDouble ();
						} else {
								checkPickupsAndPlayer ();
						}
						Gravity ();
						return false;
				}
		}
		private void checkPickupsAndPlayer ()
		{
				GameObject hitObj = castMiddle ();
				if (hitObj != null) {
							//Debug.Log("Falling object hit something 1");
							hitLogic(hitObj);
				}
		}
		//Returns object thats below the block. 
		private GameObject castMiddle ()
		{
				RaycastHit2D downInfoMiddle;
				float yPos = m_leftBottom.position.y;
				float gravSize = m_gravityLen;
				downInfoMiddle = Physics2D.Raycast (new Vector2 (transform.position.x, yPos), -Vector2.up, gravSize / 2);
				if (downInfoMiddle.collider != null) {
						GameObject hitObj = downInfoMiddle.collider.gameObject;
						return hitObj;
						/*if (hitObj.CompareTag ("PickUp")) {
								Destroy (hitObj);
						}*/
				} else {
						return null;
				}
		}

		private void hitLogic(GameObject hitObj){
			//Debug.Log("Falling object hit something 2");
			if (hitObj.CompareTag ("PickUp")&&(hitObj.name!="prefabArtifactPickUp")) {
					Destroy (hitObj);
			} else if (hitObj.CompareTag ("Player")) {
					if (m_killOnHit) {
							hitObj.gameObject.SendMessage ("killSelf");
					}
			}else if (hitObj.CompareTag("Liquid")){
					liquidController liqCtrl=hitObj.GetComponent<liquidController>();
					if (liqCtrl!=null){
						//Debug.Log("Liquid was non-null");
						liqCtrl.ifHit(this.gameObject);
						//Debug.Log("Theoretically called ifHit");
					}
			} else {
					//Debug.Log ("Hit a rock");
			}
		}

		private void checkPickupsAndPlayerDouble ()
		{
				RaycastHit2D downInfoMiddleLeft;
				RaycastHit2D downInfoMiddleRight;
				float yPos = m_leftBottom.position.y;
				float gravSize = m_gravityLen;
				float middleSize = Mathf.Abs (transform.position.x - m_leftBottom.position.x) / 2;
				downInfoMiddleLeft = Physics2D.Raycast (new Vector2 ((transform.position.x - middleSize), yPos), -Vector2.up, gravSize / 2);
				downInfoMiddleRight = Physics2D.Raycast (new Vector2 ((transform.position.x + middleSize), yPos), -Vector2.up, gravSize / 2);
		
				if (downInfoMiddleLeft.collider != null) {
						GameObject hitObj = downInfoMiddleLeft.collider.gameObject;
						if (hitObj != null) {
							hitLogic(hitObj);
						}
				}
				if (downInfoMiddleRight.collider != null) {
						GameObject hitObj = downInfoMiddleRight.collider.gameObject;
						if (hitObj != null) {
							hitLogic(hitObj);
						}
				}
		}
		private bool hitPlayer(RaycastHit2D hit){
			if (hit.collider!=null){
					return hit.collider.gameObject.CompareTag("Player");
				}else{
					return false;
				}
		}
		void Gravity ()
		{
				RaycastHit2D downInfoLeft;
				RaycastHit2D downInfoRight;
				
		
				float gravSize = m_gravityLen;
		
				Vector2 leftBottom = m_leftBottom.position;
				Vector2 rightBottom = m_rightBottom.position;
		
				float yPos = leftBottom.y;
				float xPosLeft = leftBottom.x;
				float xPosRight = rightBottom.x;
		
				//Debug.DrawLine (new Vector3 (xPosLeft, yPos, 0), new Vector3 (xPosLeft, yPos - gravSize, 0), Color.red);
				//Debug.DrawLine (new Vector3 (xPosRight, yPos, 0), new Vector3 (xPosRight, yPos - gravSize, 0), Color.blue);
		
				downInfoLeft = Physics2D.Raycast (new Vector2 (xPosLeft, yPos), -Vector2.up, gravSize);
				downInfoRight = Physics2D.Raycast (new Vector2 (xPosRight, yPos), -Vector2.up, gravSize);
						
				float leftDiff = Mathf.Abs (yPos - downInfoLeft.point.y);
				float rightDiff = Mathf.Abs (yPos - downInfoRight.point.y);
				float leastDiff;
		
				if (leftDiff < rightDiff) {
						leastDiff = leftDiff;
				} else {
						leastDiff = rightDiff;
				}
				Vector3 amountMove = (new Vector3 (0, m_gravitySpeed, 0)) * Time.fixedDeltaTime;
				bool collidersNull = downInfoLeft.collider == null && downInfoRight.collider == null;
				if ((leastDiff <= -amountMove.y) /*|| (!collidersNull)*/) {
						transform.position += (new Vector3 (0, -leastDiff, 0));
						if (hitPlayer(downInfoRight)||hitPlayer(downInfoLeft)){
							m_curState=State.Fall;
						}else{
							m_curState = State.Still;
						}
				} else if ((collidersNull) || (leastDiff >= -amountMove.y)) {
						transform.position += amountMove;
				}	

		}
	

}
