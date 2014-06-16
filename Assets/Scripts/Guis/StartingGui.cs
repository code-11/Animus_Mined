using UnityEngine;
using System.Collections;

public class StartingGui : escGui
{
		private void restOfGui ()
		{
				startingManager manager = gameObject.GetComponent<startingManager> ();
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
