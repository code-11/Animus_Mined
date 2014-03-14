using UnityEngine;
using System.Collections;

public class QuickSlot : MonoBehaviour
{		
		public Sprite curDisplay;
		void OnGUI ()
		{
				// Make a background box
				GUI.Box (new Rect (10, Screen.height - 160, 150, 150), "Quick Slot");
				inventoryManager manager = gameObject.GetComponent<inventoryManager> ();
				InvenObject chargeObj = manager.getSelObj ().GetComponent<InvenObject> ();
				if (GUI.Button (new Rect (60, Screen.height - 130, 50, 50), chargeObj.getCharges ().ToString ()))
						Debug.Log ("Clicked the button with an image");
				if (manager.getSelObj () != null) {
						
						GUI.Label (new Rect (60, Screen.height - 130 + 45, 50, 50), chargeObj.getStackName ());
				}
		
				//Debug.Log (Screen.height);
		}
}
