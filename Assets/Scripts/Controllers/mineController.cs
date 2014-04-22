using UnityEngine;
using System.Collections;

public class mineController : MonoBehaviour
{
		// Update is called once per frame
		public Transform leftMiddle;
		public Transform rightMiddle;
		public Transform middleBottom;
		public Transform middleTop;
		public int m_mineStr;
		
		void Update ()
		{
				MineLogic ();
		}
		
		void Mine (RaycastHit2D blockHit)
		{
				mineability target = blockHit.collider.gameObject.GetComponent<mineability> ();
				if (target != null) {
						target.SetHealth (target.GetHealth () - m_mineStr);
				}
		}
		
		void MineLogic ()
		{	
				float horizDir = Input.GetAxis ("Horizontal");
				float vertDir = Input.GetAxis ("Vertical");
				float vecLen = 1;
				float diff;
				RaycastHit2D hit;
				if (horizDir > 0) {
						hit = Physics2D.Raycast (new Vector2 (rightMiddle.position.x, rightMiddle.position.y), Vector2.right);
						Debug.DrawLine (rightMiddle.position, new Vector3 (rightMiddle.position.x + vecLen, rightMiddle.position.y, 0));
						diff = Mathf.Abs (rightMiddle.position.x - hit.point.x);
						if ((hit.collider != null) && (diff < .01)) {
								//Debug.Log ("Hit Right");
								Mine (hit);	
						}
				} else if (horizDir < 0) {
						hit = Physics2D.Raycast (new Vector2 (leftMiddle.position.x, leftMiddle.position.y), -Vector2.right);
						Debug.DrawLine (leftMiddle.position, new Vector3 (leftMiddle.position.x - vecLen, leftMiddle.position.y, 0));
						diff = Mathf.Abs (leftMiddle.position.x - hit.point.x);
						if ((hit.collider != null) && (diff < .01)) {
								//Debug.Log ("Hit Left");
								Mine (hit);	
						}
				} else if (vertDir > 0) {
						hit = Physics2D.Raycast (new Vector2 (middleTop.position.x, middleTop.position.y), Vector2.up);
						Debug.DrawLine (middleTop.position, new Vector3 (middleTop.position.x, middleTop.position.y + vecLen, 0));
						diff = Mathf.Abs (middleTop.position.y - hit.point.y);
						if (hit.collider != null) {
								//Debug.Log ("Hit Up");
								Mine (hit);	
						}
				} else if (vertDir < 0) {
						hit = Physics2D.Raycast (new Vector2 (middleBottom.position.x, middleBottom.position.y), -Vector2.up);
						Debug.DrawLine (middleBottom.position, new Vector3 (middleBottom.position.x, middleBottom.position.y - vecLen, 0));
						diff = Mathf.Abs (middleBottom.position.y - hit.point.y);
						if ((hit.collider != null) && (diff < .01)) {
								//Debug.Log ("Hit Down");
								Mine (hit);	
						}
				}
		}
}
