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
			//Time.timeScale = 0.3F;
			Destroy (this.gameObject);
			alertCtrl.setAlert("Backing up Consciousness. Sending Emergency Beacon.");
			Application.LoadLevel("loseScene");
			//Debug.Log ("Game Over");
		}
	}
	void checkLiquid(){
		int onlyPlayer = 1 << 9;
		int allButPlayer = ~onlyPlayer;
		Collider2D[] hit = new Collider2D[1];
 		Physics2D.OverlapPointNonAlloc (gameObject.transform.position, hit, allButPlayer);
 		if (hit[0]!=null){
 			GameObject obj=(GameObject) hit[0].gameObject;
 			if (obj.CompareTag("Liquid")){
 				liquidController liqCtrl=obj.GetComponent<liquidController>();
 				if (liqCtrl.m_killPlayerOnInside){
 					killSelf();
 				}
			}else{
				//Debug.Log("Somehow inside a non-liquid object");
			}
 		}
	}
	
	// Update is called once per frame
	void Update () {
		checkLiquid();
	}
}
