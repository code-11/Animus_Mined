using UnityEngine;
using System.Collections;

public class msgGui : MonoBehaviour
{

		private float width = 1200;
		private float height = 600;
		public float centerX;
		public float localY;
		private float leftMost;
		private msgManager manager;
		private bool ranAlready = true;
		// Use this for initialization
		
		private void Start ()
		{
				manager = gameObject.GetComponent<msgManager> ();
		}
		void OnGUI ()
		{
				runGui ();
		}
	
		private void makeBackround ()
		{
				centerX = Screen.width / 2;
				float centerY = Screen.height / 2;
				float localX = centerX - (width / 2);
				localY = centerY - (height / 2);
				leftMost = localX;
				GUI.Box (new Rect (localX, localY, width, height), "Ansible");
		}
	
		private void messageList ()
		{
				float TOPOFFSET = 30;
				GUI.Box (new Rect (leftMost, localY + TOPOFFSET, (width / 2), height - TOPOFFSET), "Messages");
				int i = 0;
				foreach (msgManager.Message msg in manager.getAllMessages()) {
						bool isSelected = i == manager.getSelected ();
						if (msg.hasBeenRead () && !isSelected) {
								GUI.color = Color.white;
						} else if (isSelected) {
								GUI.color = Color.yellow;
						} else {
								GUI.color = Color.cyan;
						}
						if (!msg.isLocked ()) {
								GUI.Button (new Rect (leftMost + 30, localY + (i * 60) + (TOPOFFSET * 2), (width / 2) - 60, 50), msg.getSubject ());
						}
						i += 1;
				}
		}
		private void messageDetail ()
		{
				GUI.color = Color.white;
				float TOPOFFSET = 30;
				GUI.Box (new Rect (centerX, localY + TOPOFFSET, (width / 2), height - TOPOFFSET), "Message Detail");
				GUI.Label (new Rect (centerX + TOPOFFSET, localY + (TOPOFFSET * 2), (width / 2) - (TOPOFFSET * 2), (height / 2) + (TOPOFFSET * 2)), manager.getSelMsg ());
		}
		private void doSelection ()
		{
				if (!ranAlready) {
						bool wDown = Input.GetKeyDown ("w");
						bool sDown = Input.GetKeyDown ("s");
						if (wDown) {
								//Debug.Log ("UP");
								manager.selectUp ();
						} else if (sDown) {
								//Debug.Log ("DOWN");
								manager.selectDown ();
						}
						ranAlready = true;
				} else {
						ranAlready = false;
				}				
		}
		void runGui ()
		{
				doSelection ();
				makeBackround ();
				messageList ();
				messageDetail ();
		}
}
