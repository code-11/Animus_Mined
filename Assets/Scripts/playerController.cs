using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour
{

		public float m_speed;
		public float m_gravity;
		// Use this for initialization
		void Start ()
		{
		
		}
		void Move ()
		{
				float horizDir = Input.GetAxis ("Horizontal");
				float vertDir = Input.GetAxis ("Vertical");
				transform.position += (new Vector3 (horizDir, vertDir) * m_speed * Time.fixedDeltaTime);
		}

		void Gravity ()
		{
				float yPos=transform.position.y;
				float xPos=transform.position.x;
				Debug.DrawLine(new Vector3(xPos+.5F,yPos-.5F,0),new Vector3(xPos+.5F,yPos-1.5F,0),Color.red);
				Debug.DrawLine(new Vector3(xPos-.5F,yPos-.5F,0),new Vector3(xPos-.5F,yPos-1.5F,0),Color.blue);
				RaycastHit2D downInfoLeft;
				RaycastHit2D downInfoRight;
				downInfoLeft=Physics2D.Raycast(new Vector2(xPos-.5F,yPos-.5F),-Vector2.up,1F);
				downInfoRight=Physics2D.Raycast(new Vector2(xPos+.5F, yPos-.5F),-Vector2.up,1F);
				Debug.Log(downInfoLeft.collider);
				if (downInfoLeft.collider==null && downInfoRight.collider==null){
					transform.position+= (new Vector3(0,m_gravity,0))*Time.fixedDeltaTime;
				}
				
		}	


		void FixedUpdate ()
		{
				Move ();
				Gravity();
		}
}
