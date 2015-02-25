using UnityEngine;
using System.Collections;

public class deathController : MonoBehaviour {
	private alertManager alertCtrl;

	// Use this for initialization
	void Start () {
		alertCtrl=gameObject.GetComponent<alertManager>();

	} 
	void killSelf ()
	{
		GameObject a_replicator=GameObject.Find("prefabReplicator");
		if (a_replicator!=null){
			alertCtrl.setAlert("Uploading Neural Net");
			gameObject.transform.position=a_replicator.transform.position;
			replicatorController repr =a_replicator.GetComponent<replicatorController>();
			repr.lessBody();
		}else{
			Time.timeScale = 0.3F;
			Destroy (this.gameObject);
			alertCtrl.setAlert("Backing up Consciousness. Sending Emergency Beacon.");
			Debug.Log ("Game Over");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
