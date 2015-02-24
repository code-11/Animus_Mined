using UnityEngine;
using System.Collections;

public class deathController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void killSelf ()
	{
		Time.timeScale = 0.3F;
		Destroy (this.gameObject);
		Debug.Log ("Game Over");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
