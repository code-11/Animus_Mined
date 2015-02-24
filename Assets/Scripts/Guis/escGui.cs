using UnityEngine;
using System.Collections;

public class escGui : MonoBehaviour
{
		private float width = 400;
		private float height = 400;
		public float centerX;
		public float localY;
		private float centerY;
		private float localX;
		private escManager manager;
		// Use this for initialization
		void Start ()
		{
				manager=gameObject.GetComponent<escManager> ();
				//Calculate useful parts of the the screen
				centerX = Screen.width / 2;
				centerY = Screen.height / 2;
				localX = centerX - (width / 2);
				localY = centerY - (height / 2);
		}
		void OnGUI ()
		{
				runGui ();
		}
		
		private void makeBackround ()
		{

				GUI.Box (new Rect (localX, localY, width, height), "Menu");
		}
	
		private void restOfGui (int vertOffset)
		{
				escManager manager = gameObject.GetComponent<escManager> ();
				if (manager != null) {
						ArrayList items = manager.getItems ();
						int i = 0;
						foreach (escManager.menuItem item in items) {
								if (i == manager.getSelNum ()) {
										GUI.color = Color.yellow;
								} else {
										GUI.color = Color.white;
								}
								float btnX = centerX - 100;
								float btnY = localY + (i + 1) * 55+vertOffset;
								if (GUI.Button (new Rect (btnX, btnY, 200, 50), item.getDesc ()))
										Debug.Log ("Clicked the button with an imag");
								i += 1;
						}
				}
		}
	
		void runGui ()
		{
			if (manager.startingGame){
				restOfGui (100);
			}else{
				makeBackround ();
				restOfGui (0);
			}
				
		}
}
