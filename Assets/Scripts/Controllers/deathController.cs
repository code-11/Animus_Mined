using UnityEngine;
using System.Collections;

public class deathController : MonoBehaviour {
	private alertManager alertCtrl;
	private inventoryManager invenCtrl;
	public float m_deathDelay;
	public bool m_killCamUp;
	// Use this for initialization
	void Start () {
		alertCtrl=gameObject.GetComponent<alertManager>();
		invenCtrl=gameObject.GetComponent<inventoryManager>();

	} 
	public void killSelf ()
	{
		invenCtrl.resetInven();
		GameObject a_replicator=GameObject.Find("prefabReplicator");
		if (a_replicator!=null){
			alertCtrl.setAlert("Uploading Neural Net");

			StartCoroutine (killCam ());

		}else{
			//Time.timeScale = 0.3F;
			lose();
		}
	}
	private void reestablish(){
		GameObject a_replicator=GameObject.Find("prefabReplicator");
		if(a_replicator!=null){
				this.gameObject.transform.position=a_replicator.transform.position;
				replicatorController repr =a_replicator.GetComponent<replicatorController>();
				repr.lessBody();
			}else{
				Debug.Log("replicator destroyed during kill cam somehow");
			}
	}
	public bool getKillCamUp(){
		return m_killCamUp;
	}
	private void zDisplace(){
		Vector3 curPos=gameObject.transform.position;
		float x=curPos.x;
		float y=curPos.y;
		float z=curPos.z;
		Vector3 newPos=new Vector3(x,y,z-1);
		gameObject.transform.position=newPos;
	}
	private IEnumerator killCam(){
		
		//Time.timeScale = 0.3F;
		SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer> ();
		BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D> ();
		GameObject ghost= (transform.Find("ghost")).gameObject;
		SpriteRenderer grenderer = ghost.GetComponent<SpriteRenderer>();

		m_killCamUp=true;
		renderer.enabled=false;
		collider.enabled=false;
		zDisplace();
		grenderer.enabled=true;
		//Debug.Log("TEst");
		StartCoroutine (stretch (ghost));
		yield return new WaitForSeconds (m_deathDelay);
		//Time.timeScale = 1F;
		grenderer.enabled=false;
		renderer.enabled=true;
		collider.enabled=true;
		m_killCamUp=false;
		reestablish();

	}
	private IEnumerator stretch(GameObject ghost){
		float delta=.01f;
		float total=m_deathDelay/4;
		SpriteRenderer grenderer = ghost.GetComponent<SpriteRenderer>();

		Vector3 start=ghost.transform.localScale;
		Vector3 end= new Vector3(start.x,start.y+5,start.z);
		while (total>0){
			yield return new WaitForSeconds(delta);
			total-=delta;
			ghost.transform.localScale = Vector3.Lerp (end, start, total / (m_deathDelay/4));
			grenderer.color= new Color(1f,1f,1f,Mathf.Lerp(0f,1f,total/(m_deathDelay/4)));
		}
		
		grenderer.enabled=false;
		ghost.transform.localScale =start;
	}
	public void lose(){
			Destroy (this.gameObject);
			alertCtrl.setAlert("Backing up Consciousness. Sending Emergency Beacon.");
			Application.LoadLevel("loseScene");
	}
	public void win(){
			Destroy(this.gameObject);
			alertCtrl.setAlert("Backing up Consciousness. Sending Recovery Beacon.");
			Application.LoadLevel("winScene");
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
		if (!m_killCamUp){
			checkLiquid();
		}
	}
}
