using UnityEngine;
using System.Collections;

public class onPickUpResearch : behaviorOnPickup
{
		public int m_researchNum;
		public override void onPickup (GameObject player)
		{
				unlockResearch (m_researchNum, player);
				Debug.Log ("picking up2");
		}
		private void unlockResearch (int researchNum, GameObject player)
		{
				craftManager manager = player.GetComponent<craftManager> ();
				if (manager != null) {
						manager.activate (researchNum);
						Debug.Log ("Recipe number " + researchNum + " unlocked");
				} else {
						Debug.Log ("Player has no craft manager somehow");
				}
		}
}
