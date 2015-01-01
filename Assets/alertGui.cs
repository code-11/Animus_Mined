using UnityEngine;
using System.Collections;

public class alertGui : MonoBehaviour
{


		public string m_alert;
		// Use this for initialization
		void Start ()
		{
				m_alert = "";
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		void OnGUI ()
		{
				// Make a background box
				int buffer = 20;
				GUI.Box (new Rect (buffer, Screen.height - 2 * buffer, Screen.width - 2 * buffer, 30), m_alert);					
		
				//Debug.Log (Screen.height);
		}
		void setAlert (string newAlert)
		{
				m_alert = newAlert;
		}
}
