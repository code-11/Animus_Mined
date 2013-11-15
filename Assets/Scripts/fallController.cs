﻿using UnityEngine;
using System.Collections;

public class fallController : MonoBehaviour
{
		public Transform m_leftBottom;
		public Transform m_rightBottom;
		public float m_gravity;
		void Gravity ()
		{
				RaycastHit2D downInfoLeft;
				RaycastHit2D downInfoRight;
		
				float gravSize = 1F;
		
				Vector2 leftBottom = m_leftBottom.position;
				Vector2 rightBottom = m_rightBottom.position;
		
				float yPos = leftBottom.y;
				float xPosLeft = leftBottom.x;
				float xPosRight = rightBottom.x;
		
				Debug.DrawLine (new Vector3 (xPosLeft, yPos, 0), new Vector3 (xPosLeft, yPos - gravSize, 0), Color.red);
				Debug.DrawLine (new Vector3 (xPosRight, yPos, 0), new Vector3 (xPosRight, yPos - gravSize, 0), Color.blue);
		
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
				Vector3 amountMove = (new Vector3 (0, m_gravity, 0)) * Time.fixedDeltaTime;
		
				bool collidersNull = downInfoLeft.collider == null && downInfoRight.collider == null;
/*				Debug.Log (downInfoLeft.collider);
				Debug.Log (downInfoRight.collider);*/
				if ((leastDiff <= -amountMove.y) && (!collidersNull)) {
						transform.position += (new Vector3 (0, -leastDiff, 0));
						//Debug.Log ("Just a bit more");
				} else if ((collidersNull) || (leastDiff >= -amountMove.y)) {
						transform.position += amountMove;
						//Debug.Log ("Apply gravity like regular");
				}	
		}	
	
		// Update is called once per frame
		void FixedUpdate ()
		{
				Gravity ();
		}
}
