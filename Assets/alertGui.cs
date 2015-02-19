using UnityEngine;
using System.Collections;

public class alertGui : MonoBehaviour
{


		public string m_alert;
		public float m_lastAlertTime=0;
		// Use this for initialization
		void Start ()
		{
				setAlert("Started Game");
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		public string timeSinceLast(){
			float returnTime=Mathf.Round(Time.time-m_lastAlertTime);
			if(returnTime<=59){
				return returnTime+" sec";
			}else{
				return Mathf.Floor(returnTime/60)+" min : "+(returnTime % 60)+" sec";
			} 
		}
		void OnGUI ()
		{
				// Make a background box
				int buffer = 20;
				GUI.Box (new Rect (buffer, /*Screen.height - 2 * buffer*/0, Screen.width - 2 * buffer, 30), m_alert+" "+timeSinceLast());					
		
				//Debug.Log (Screen.height);
		}
		public void setAlert (string newAlert)
		{
				m_lastAlertTime=Time.time;
				m_alert = newAlert;
		}
}
