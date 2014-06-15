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
		
		void Mine (Collider2D blockHit)
		{
				mineability target = blockHit.gameObject.GetComponent<mineability> ();
				if (target != null) {
						target.SetHealth (target.GetHealth () - m_mineStr);
				}
		}
		
		void MineLogic ()
		{	
				float horizDir = Input.GetAxis ("Horizontal");
				float vertDir = Input.GetAxis ("Vertical");
				float vecLen = 1;
				Collider2D[] hits = new Collider2D[1];
				if (horizDir > 0) {
						Physics2D.OverlapPointNonAlloc (new Vector2 (transform.position.x + vecLen, transform.position.y), hits);
						if (hits [0] != null) { 
								
								Mine (hits [0]);	
						}
				} else if (horizDir < 0) {
						Physics2D.OverlapPointNonAlloc (new Vector2 (transform.position.x - vecLen, transform.position.y), hits);
						if (hits [0] != null) {
								//Debug.Log ("Hit Left");
								Mine (hits [0]);	
						}
				} else if (vertDir > 0) {
						Physics2D.OverlapPointNonAlloc (new Vector2 (transform.position.x, transform.position.y + vecLen), hits);
						if (hits [0] != null) {
								//Debug.Log ("Hit Up");
								Mine (hits [0]);	
						}
				} else if (vertDir < 0) {
						Physics2D.OverlapPointNonAlloc (new Vector2 (transform.position.x, transform.position.y - vecLen), hits);
						if (hits [0] != null) {
								//Debug.Log ("Hit Down");
								Mine (hits [0]);	
						}
				}
		}
}
