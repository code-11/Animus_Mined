using UnityEngine;
using System.Collections;

public class mineController : MonoBehaviour
{
		// Update is called once per frame
		public Transform leftMiddle;
		public Transform rightMiddle;
		public Transform middleBottom;
		public Transform middleTop;
		public LineRenderer mineLaser;
		public int m_mineStr;
		void Start ()
		{
				mineLaser = gameObject.AddComponent<LineRenderer> ();
				mineLaser.material = new Material (Shader.Find ("Sprites/Default"));
				mineLaser.material.color = Color.red;	
				mineLaser.sortingOrder = 1;
				mineLaser.SetWidth (0.05F, 0.05F);
				mineLaser.SetVertexCount (2);
		}
		void Update ()
		{
				craftManager craft = gameObject.GetComponent<craftManager> ();
				msgManager message = gameObject.GetComponent<msgManager> ();
				escManager escape = gameObject.GetComponent<escManager> ();
				deathController deathMan= gameObject.GetComponent<deathController>();
				if ((craft != null) && (message != null) && (escape != null)&&(deathMan!=null)) {
						if ((!craft.getGuiMenuUp ()) && (!message.getMsgGuiUp ()) && (!escape.getEscMenuUp ())&&(!deathMan.getKillCamUp())) {
								MineLogic ();
						}
				}
				
		}
		
		void Mine (Collider2D blockHit)
		{
				mineability target = blockHit.gameObject.GetComponent<mineability> ();
				if (target != null) {
						mineLaser.enabled = true;
						target.SetHealth (target.GetHealth () - m_mineStr);
						mineLaser.SetPosition (0, target.transform.position);
						mineLaser.SetPosition (1, gameObject.transform.position);
				}else{
					mineLaser.enabled=false;
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
						}else{
							mineLaser.enabled=false;
						}
				} else if (horizDir < 0) {
						Physics2D.OverlapPointNonAlloc (new Vector2 (transform.position.x - vecLen, transform.position.y), hits);
						if (hits [0] != null) {
								//Debug.Log ("Hit Left");
								Mine (hits [0]);	
						}else{
							mineLaser.enabled=false;
						}
				} else if (vertDir > 0) {
						Physics2D.OverlapPointNonAlloc (new Vector2 (transform.position.x, transform.position.y + vecLen), hits);
						if (hits [0] != null) {
								//Debug.Log ("Hit Up");
								Mine (hits [0]);	
						}else{
							mineLaser.enabled=false;
						}
				} else if (vertDir < 0) {
						Physics2D.OverlapPointNonAlloc (new Vector2 (transform.position.x, transform.position.y - vecLen), hits);
						if (hits [0] != null) {
								//Debug.Log ("Hit Down");
								Mine (hits [0]);	
						}else{
							mineLaser.enabled=false;
						}
				} else {
						mineLaser.enabled = false;
				}
		}
}
