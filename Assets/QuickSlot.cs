using UnityEngine;
using System.Collections;

public class QuickSlot : MonoBehaviour
{		
		public Sprite curDisplay;
		void OnGUI ()
		{
				// Make a background box
				GUI.Box (new Rect (10, Screen.height - 160, 150, 150), "Quick Slot");
				//Debug.Log (Screen.height);
		}
}
