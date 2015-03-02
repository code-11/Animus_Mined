using UnityEngine;
using System.Collections;

public class replicatorController : MonoBehaviour {

	public int m_bodies=3;
	public void lessBody(){
		Debug.Log("Reducing Bodies");
		m_bodies-=1;
		if (m_bodies<=0){
			Destroy (this.gameObject);
		}
	}
}
