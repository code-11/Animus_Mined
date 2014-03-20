using UnityEngine;
using System.Collections;

public class SupportOnUse : Usability
{

		private GameObject m_support;
	
		void OnEnable ()
		{
				m_support = Resources.Load ("prefabSupport") as GameObject;
		}
		public override void Use (Transform playerPos)
		{
				//Debug.Log ("Ladder was used");
		
				int x = (int)Mathf.Round (playerPos.position.x);
				int y = (int)Mathf.Round (playerPos.position.y);
				//Debug.Log ("PlayerPosx: " + playerPos.position.x + " Roundedx: " + x);
				Instantiate (m_support, new Vector3 (x, y, 0), Quaternion.identity);
		}
}
