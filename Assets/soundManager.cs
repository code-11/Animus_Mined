using UnityEngine;
using System.Collections;

public class soundManager : MonoBehaviour {

	public AudioClip m_craftSuccess;
	public AudioClip m_genFailure;
	public AudioClip m_laser;
	public AudioSource m_source;
	// Use this for initialization
	void Start () {
		m_source= gameObject.AddComponent<AudioSource>();
		m_craftSuccess=Resources.Load("Sounds/Metal_Drop") as AudioClip;
		m_genFailure=Resources.Load("Sounds/error")as AudioClip;
		m_laser=Resources.Load("Sounds/laser")as AudioClip;
		if (m_genFailure==null){
			Debug.Log("NULL");
		}
	}
	public void playCraftSuccess(){
		AudioSource.PlayClipAtPoint(m_craftSuccess,gameObject.transform.position);

	}
	public void playGenFail(){
		AudioSource.PlayClipAtPoint(m_genFailure,gameObject.transform.position);
		//m_source.clip = m_genFailure;
		//m_source.Play();
	}
	public void playLaser(){
		m_source.clip = m_laser;
		m_source.Play();
	}
}
