using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour
{

		public float m_speed;
		public Transform leftHigh;
		public Transform leftLow;
		public Transform rightHigh;
		public Transform rightLow;
		//public int m_mineStr;
		// Use this for initialization
		void MoveHorizNew ()
		{
				float rayScale = .25F;
				float horizDir = Input.GetAxis ("Horizontal");
				//Debug.Log (horizDir);
				//float vertDir = Input.GetAxis ("Vertical");
			
				RaycastHit2D infoHLeft;
				RaycastHit2D infoLLeft;
				RaycastHit2D infoHRight;
				RaycastHit2D infoLRight;
				
				float closeLeftDiff;
				float closeRightDiff;
				RaycastHit2D closeLeftInfo;
				RaycastHit2D closeRightInfo;
			
				Vector3 leftHPos = leftHigh.position - (new Vector3 (0, .01f, 0));
				Vector3 leftLPos = leftLow.position + (new Vector3 (0, .01f, 0));
				Vector3 rightHPos = rightHigh.position - (new Vector3 (0, .01f, 0));
				Vector3 rightLPos = rightLow.position + (new Vector3 (0, .01f, 0));
			
				infoHLeft = Physics2D.Raycast (new Vector2 (leftHPos.x, leftHPos.y), -Vector2.right, m_speed * rayScale);
				infoHRight = Physics2D.Raycast (new Vector2 (rightHPos.x, rightHPos.y), Vector2.right, m_speed * rayScale);
				infoLLeft = Physics2D.Raycast (new Vector2 (leftLPos.x, leftLPos.y), -Vector2.right, m_speed * rayScale);
				infoLRight = Physics2D.Raycast (new Vector2 (rightLPos.x, rightLPos.y), Vector2.right, m_speed * rayScale);			
				//Debug.DrawLine (new Vector3 (xPosRight, yPos, 0), new Vector3 (xPosRight, yPos - gravSize, 0), Color.blue);
				Vector3 amountMove = (new Vector3 (horizDir, 0f, 0f) * m_speed * Time.fixedDeltaTime);
				//Vector3 amountMove = (new Vector3 (.1f, 0f, 0f));
				//Debug.Log ("Amountmove: " + amountMove);
				float leftHDiff = Mathf.Abs (leftHPos.x - infoHLeft.point.x);
				float rightHDiff = Mathf.Abs (rightHPos.x - infoHRight.point.x);
				float leftLDiff = Mathf.Abs (leftLPos.x - infoLLeft.point.x);
				float rightLDiff = Mathf.Abs (rightLPos.x - infoLRight.point.x);
			

				if (leftHDiff > leftLDiff) {
						closeLeftDiff = leftLDiff;
						closeLeftInfo = infoLLeft;
				} else {
						closeLeftDiff = leftHDiff;
						closeLeftInfo = infoHLeft;
				}
				if (rightHDiff > rightLDiff) {
						closeRightDiff = rightLDiff;
						closeRightInfo = infoLRight;
				} else {
						closeRightDiff = rightHDiff;
						closeRightInfo = infoHRight;
				}
				
			
				if ((closeRightInfo.collider != null) && (horizDir > 0) && (closeRightDiff < Mathf.Abs (amountMove.x))) {
						if (! closeRightInfo.collider.gameObject.GetComponent<permeability> ().m_horizontal) {
								//Debug.Log ("RightIf" + closeRightDiff);
								transform.position += (new Vector3 (closeRightDiff, 0, 0));
						} else {
								//Debug.Log ("Right");
								transform.position += amountMove;
						}
						Debug.Log (closeRightInfo.collider.gameObject.name);
						//Debug.DrawLine (rightHPos, rightHPos + (new Vector3 (m_speed * rayScale, 0, 0)), Color.green);
				} else if ((closeLeftInfo.collider != null) && (horizDir < 0) && (closeLeftDiff < Mathf.Abs (amountMove.x))) {
						if (! closeLeftInfo.collider.gameObject.GetComponent<permeability> ().m_horizontal) {
								transform.position -= (new Vector3 (closeLeftDiff, 0, 0));
								//Debug.Log ("LeftIf: " + closeLeftDiff);
						} else {
								//Debug.Log ("Left");
								transform.position += amountMove;
						}
						Debug.Log (closeLeftInfo.collider.gameObject.name);
						//Debug.Log ("Left Diff" + leftDiff);
						//Debug.Log ("Amount Move" + Mathf.Abs (amountMove.x));
						//Debug.DrawLine (leftPos, leftPos - (new Vector3 (m_speed * rayScale, 0, 0)), Color.green);
				} else {
						//Debug.Log ("else: " + amountMove);
						transform.position += amountMove;
				}
		
				//MineLogic (infoRight, infoLeft, leftDiff, rightDiff, horizDir);
		}	
		/*void MoveHoriz ()
		{
				float rayScale = .25F;
				float horizDir = Input.GetAxis ("Horizontal");
				//float vertDir = Input.GetAxis ("Vertical");
				
				RaycastHit2D infoLeft;
				RaycastHit2D infoRight;
				
				Vector3 leftPos = left.position;
				Vector3 rightPos = right.position;
				
				infoLeft = Physics2D.Raycast (new Vector2 (leftPos.x, leftPos.y), -Vector2.right, m_speed * rayScale);
				infoRight = Physics2D.Raycast (new Vector2 (rightPos.x, rightPos.y), Vector2.right, m_speed * rayScale);
		
				//Debug.DrawLine (new Vector3 (xPosRight, yPos, 0), new Vector3 (xPosRight, yPos - gravSize, 0), Color.blue);
				Vector3 amountMove = (new Vector3 (horizDir, 0) * m_speed * Time.fixedDeltaTime);
				
				float leftDiff = Mathf.Abs (leftPos.x - infoLeft.point.x);
				float rightDiff = Mathf.Abs (rightPos.x - infoRight.point.x);
				
								
																
				if ((infoRight.collider != null) && (horizDir > 0) && (rightDiff < Mathf.Abs (amountMove.x))) {
						if (! infoRight.collider.gameObject.GetComponent<permeability> ().m_horizontal) 
								transform.position += (new Vector3 (rightDiff, 0, 0));
						else
								transform.position += amountMove;
						//Debug.DrawLine (rightPos, rightPos + (new Vector3 (m_speed * rayScale, 0, 0)), Color.green);
				} else if ((infoLeft.collider != null) && (horizDir < 0) && (leftDiff < Mathf.Abs (amountMove.x))) {
						if (! infoLeft.collider.gameObject.GetComponent<permeability> ().m_horizontal)
								transform.position -= (new Vector3 (leftDiff, 0, 0));
						else
								transform.position += amountMove;
						//Debug.Log ("Left Diff" + leftDiff);
						//Debug.Log ("Amount Move" + Mathf.Abs (amountMove.x));
						//Debug.DrawLine (leftPos, leftPos - (new Vector3 (m_speed * rayScale, 0, 0)), Color.green);
				} else {
						transform.position += amountMove;
				}
				
				//MineLogic (infoRight, infoLeft, leftDiff, rightDiff, horizDir);
		}*/
		/*
		void MineLogic (RaycastHit2D infoRight, RaycastHit2D infoLeft, float leftDiff, float rightDiff, float horizDir)
		{	
				if (horizDir != 0) {
						if ((infoLeft.collider != null) && (leftDiff <= .01) && (horizDir < 0)) {
								Mine (infoLeft);
						} else if ((infoRight.collider != null) && (rightDiff <= .01) && (horizDir > 0)) {
								Mine (infoRight);
						}
				}
		}
		void Mine (RaycastHit2D blockHit)
		{
				mineability target = blockHit.collider.gameObject.GetComponent<mineability> ();
				target.SetHealth (target.GetHealth () - m_mineStr);
				
				//Destroy (blockHit.collider.gameObject);
		}
		*/
		void killSelf ()
		{
				Time.timeScale = 0.3F;
				Destroy (this.gameObject);
				Debug.Log ("Game Over");
		}
		void Update ()
		{
				//Debug.Log (Input.GetAxis ("Horizontal"));
				MoveHorizNew ();
				//Gravity ();
		}
}
