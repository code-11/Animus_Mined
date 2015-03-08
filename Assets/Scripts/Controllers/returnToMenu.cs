using UnityEngine;
using System.Collections;

public class returnToMenu : MonoBehaviour {
	public float m_delay;
	private bool m_delaying;
	// Use this for initialization
	void Start () {
		m_delay=4f;
		StartCoroutine (menuDelay());
	}
	IEnumerator menuDelay(){
		m_delaying=true;
		yield return new WaitForSeconds (m_delay);
		m_delaying=false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey && (m_delaying==false)){
			Application.LoadLevel("startingScene");
		}
	}
}
