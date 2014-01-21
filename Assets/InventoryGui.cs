using UnityEngine;
using System.Collections;

public class InventoryGui : MonoBehaviour
{
		public Texture2D btnTexture;
		
		void OnGUI ()
		{
				GUI.Box (new Rect (10, 10, 300, 300), "Inventory");
				inventoryManager manager = gameObject.GetComponent<inventoryManager> ();
				int x = 30;
				int y = 30;
				
				
				foreach (var item in manager.getInven().Values) {
						if (GUI.Button (new Rect (x, y, 50, 50), btnTexture))
								Debug.Log ("Clicked the button with an image");
						GUI.Label (new Rect (x + 10, y + 30, 50, 50), ((GameObject)item).name);
						x = x + 60;
				}


		
				
				
		}
}
