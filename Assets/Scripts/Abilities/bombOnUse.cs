﻿using UnityEngine;
using System.Collections;

public class bombOnUse : Usability
{
		public float m_explosionSize;
		private GameObject m_bomb;
		void OnEnable ()
		{
				m_bomb = Resources.Load ("prefabBomb") as GameObject;
		}
		public override void Use (Transform playerPos)
		{
				int x = (int)Mathf.Round (playerPos.position.x);
				int y = (int)Mathf.Round (playerPos.position.y);
				//Debug.Log ("PlayerPosx: " + playerPos.position.x + " Roundedx: " + x);
				GameObject theBomb = (GameObject)Instantiate (m_bomb, new Vector3 (x, y, 0), Quaternion.identity);
				bombController bombLogic = theBomb.GetComponent<bombController> ();
				bombLogic.setExplosionSize (m_explosionSize);
		}

		
}
