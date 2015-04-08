using UnityEngine;
using System.Collections;

public class replicatorController : MonoBehaviour {

	public Sprite one;
	public Sprite zero;
	private SpriteRenderer renderer;
	public int m_bodies=3;

	public void Start(){
		renderer=gameObject.GetComponent<SpriteRenderer>();
	}

	public void lessBody(){
		Debug.Log("Reducing Bodies");
		m_bodies-=1;
		if(m_bodies==2){
			renderer.sprite=one;
		}
		if(m_bodies==1){
			renderer.sprite=zero;
		}
		if (m_bodies<=0){
			Destroy (this.gameObject);
		}
	}
}
