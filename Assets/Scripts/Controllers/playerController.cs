using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour
{

		public float m_speed;
		public Transform left;
		public Transform right;
		//public int m_mineStr;
		// Use this for initialization
		void MoveHoriz ()
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
		}
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
		void FixedUpdate ()
		{
				MoveHoriz ();
				//Gravity ();
		}
}
