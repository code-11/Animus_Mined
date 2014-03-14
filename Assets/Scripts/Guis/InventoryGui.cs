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
				
				int i = 0;
				if (manager.getInven ().Count > 0) {
						foreach (GameObject item in manager.getInven()) {
								if (i == manager.getSelNum ()) {
										GUI.color = Color.yellow;
								} else {
										GUI.color = Color.white;
								}
								InvenObject chargeObj = ((GameObject)item).GetComponent<InvenObject> ();
								if (GUI.Button (new Rect (x, y, 50, 50), chargeObj.getCharges ().ToString ()))
										Debug.Log ("Clicked the button with an image");
								
								GUI.Label (new Rect (x, y + 45, 50, 50), (chargeObj.getStackName ()));
								x = x + 60;
								i += 1;
						}
				}


		
				
				
		}
}
