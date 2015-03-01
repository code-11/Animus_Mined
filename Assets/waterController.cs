using UnityEngine;
using System.Collections;

public class waterController : liquidController {

	// Use this for initialization
	void Start () {
		m_toSpread="prefabWater";
		m_spreadDelaySlow=10f;
		m_spreadDelayFast=1f;
		m_waiting = false;
		m_spread=true;
		m_killPlayerOnInside=true;
	}
	public override void ifHit(GameObject obj){
		//Debug.Log("calling destroy");
		Destroy(this.gameObject);
	}
}
