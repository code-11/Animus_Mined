using UnityEngine;
using System.Collections;

public class InventoryGui : MonoBehaviour
{
		public Texture2D btnTexture;
		//public GUIStyle highlighted;
		void OnGUI ()
		{
				GUI.Box (new Rect (170, Screen.height - 110, 1000, 100), "Inventory");
				inventoryManager manager = gameObject.GetComponent<inventoryManager> ();
				int x = 190;
				int y = Screen.height - 90;
				
				int index = 0;
				foreach (var item in manager.getInven().Values) {
						if (index == manager.getSelNum ()) {
								GUI.color = Color.yellow;
						} else {
								GUI.color = Color.white;
						}
						if (GUI.Button (new Rect (x, y, 50, 50), btnTexture))
								Debug.Log ("Clicked the button with an image");
						GUI.Label (new Rect (x, y + 45, 50, 50), ((GameObject)item).name);
						x = x + 60;
						index += 1;
				}


		
				
				
		}
}
