using UnityEngine;
using System.Collections;

public class alertGui : MonoBehaviour
{
		private alertManager manager;
		// Use this for initialization
		void Start ()
		{
				manager=gameObject.GetComponent<alertManager>();
		}
		void OnGUI ()
		{
				// Make a background box
				int buffer = 20;
				GUI.Box (new Rect (buffer, 0, Screen.width - 2 * buffer, 30), manager.getAlert()+" "+manager.timeSinceLast());					
		}

}
