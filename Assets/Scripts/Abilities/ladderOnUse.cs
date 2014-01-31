using UnityEngine;
using System.Collections;

public class ladderOnUse : Usability
{
		private GameObject m_ladder;
		
		void OnEnable ()
		{
				m_ladder = Resources.Load ("prefabLadder") as GameObject;
		}
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		public override void Use (Transform playerPos)
		{
				//Debug.Log ("Ladder was used");
				
				int x = (int)Mathf.Round (playerPos.position.x);
				int y = (int)Mathf.Round (playerPos.position.y);
				//Debug.Log ("PlayerPosx: " + playerPos.position.x + " Roundedx: " + x);
				Instantiate (m_ladder, new Vector3 (x, y, 0), Quaternion.identity);
		}
}
