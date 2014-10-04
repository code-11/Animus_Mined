using UnityEngine;
using System.Collections;

public class onPickUpMessage : behaviorOnPickup
{
		public int m_messageNum;
		public override void onPickup (GameObject player)
		{
				unlockMessage (m_messageNum, player);
				//Debug.Log ("picking up2");
		}
		private void unlockMessage (int messageNum, GameObject player)
		{
				msgManager manager = player.GetComponent<msgManager> ();
				if (manager != null) {
						manager.activate (messageNum);
						Debug.Log ("Message number " + messageNum + " unlocked");
				} else {
						Debug.Log ("Player has no craft manager somehow");
				}
		}
}
