using UnityEngine;
using System.Collections;

public class alertManager : MonoBehaviour {

	public string m_alert;
	public float m_lastAlertTime=0;
	private alertGui gui;
	private InputManager inCtrl;

	void Start ()
	{
			gui= gameObject.GetComponent<alertGui>();
			setAlert("Started Game");
			inCtrl=gameObject.GetComponent<InputManager>();

	}
	void Update () {
		if (inCtrl.alertPress()){
			if (gui.enabled == true) {
					gui.enabled = false;
			} else {
					gui.enabled = true;
			}
		}
	}

	public string timeSinceLast(){
		float returnTime=Mathf.Round(Time.time-m_lastAlertTime);
		if(returnTime<=59){
			return returnTime+" sec";
		}else{
			return Mathf.Floor(returnTime/60)+" min : "+(returnTime % 60)+" sec";
		} 
	}	
	public void setAlert (string newAlert)
	{
			m_lastAlertTime=Time.time;
			m_alert = newAlert;
	}
	public string getAlert(){
			return m_alert;
	}
}
