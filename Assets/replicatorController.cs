using UnityEngine;
using System.Collections;

public class replicatorController : MonoBehaviour {

	private int m_bodies=3;
	public void lessBody(){
		m_bodies-=1;
		if (m_bodies<=0){
			Destroy (this.gameObject);
		}
	}
}
