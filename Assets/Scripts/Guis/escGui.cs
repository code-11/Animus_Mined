using UnityEngine;
using System.Collections;

public class escGui : MonoBehaviour
{
		private float width = 400;
		private float height = 400;
		// Use this for initialization
		void OnGUI ()
		{
				runGui ();
		}
		void runGui ()
		{
				float centerX = Screen.width / 2;
				float centerY = Screen.height / 2;
				float localX = centerX - (width / 2);
				float localY = centerY - (height / 2);
				GUI.Box (new Rect (localX, localY, width, height), "Menu");
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
								float btnY = localY + (i + 1) * 55;
								if (GUI.Button (new Rect (btnX, btnY, 200, 50), item.getDesc ()))
										Debug.Log ("Clicked the button with an imag");
								i += 1;
						}
				}
		}
}
