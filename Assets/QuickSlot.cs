﻿using UnityEngine;
using System.Collections;

public class QuickSlot : MonoBehaviour
{		
		public Sprite curDisplay;
		void OnGUI ()
		{
				// Make a background box
				GUI.Box (new Rect (10, Screen.height - 160, 150, 150), "Quick Slot");
				inventoryManager manager = gameObject.GetComponent<inventoryManager> ();
				if (GUI.Button (new Rect (60, Screen.height - 130, 50, 50), ""))
						Debug.Log ("Clicked the button with an image");
				if (manager.getSelObj () != null) {
						GUI.Label (new Rect (60, Screen.height - 130 + 45, 50, 50), manager.getSelObj ().name);
				}
		
				//Debug.Log (Screen.height);
		}
}
