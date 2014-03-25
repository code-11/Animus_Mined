using UnityEngine;
using System.Collections;

public class DiscreteSpring : MonoBehaviour
{

		public float m_movementThreshold;
		public float m_stepSize;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				Springify ();
		}
		void Springify ()
		{
				float vertDir = Input.GetAxis ("Vertical");
				float horzDir = Input.GetAxis ("Horizontal");
				float movement = Mathf.Abs (vertDir) + Mathf.Abs (horzDir);
				if (movement < m_movementThreshold) {
						Vector3 pos = transform.position;
						int disPosX = Mathf.RoundToInt (pos.x);
						int disPosY = Mathf.RoundToInt (pos.y);
						Debug.Log ("posx " + pos.x + " rounded " + disPosX);
						float diffX = Mathf.Abs (pos.x - disPosX);
						float diffY = Mathf.Abs (pos.y - disPosY);
						float xSign = -Mathf.Sign (pos.x - disPosX);
						//Debug.Log (Mathf.RoundToInt (.2f));
						
						if (diffX > m_stepSize) {
								transform.position += new Vector3 ((m_stepSize * xSign), 0, 0);
						} else {
								transform.position += new Vector3 (diffX, 0, 0);
						}
						
						
				}
		}
}
