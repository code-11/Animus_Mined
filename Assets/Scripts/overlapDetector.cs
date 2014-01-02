using UnityEngine;
using System.Collections;

public class overlapDetector : MonoBehaviour
{

	private Hashtable m_olapObjs;
	public float m_checkRange;
	private bool m_check = true;
	void Start ()
	{
		m_olapObjs = new Hashtable ();
	}
	// Update is called once per frame
	void Update ()
	{	
		if (m_check) {
			m_check = false;
			//Debug.Log ("Inside");
			StartCoroutine (checkOverlap ());
			//checkOverlap ();
		}
	}
	IEnumerator checkOverlap ()
	{
		yield return new WaitForSeconds (0.5f);
		m_check = true;
		Collider2D[] coll = new Collider2D[5];
		float scale = m_checkRange;
		int numHit = Physics2D.OverlapCircleNonAlloc ((Vector2)transform.position, scale, coll, Physics.AllLayers);
		for (int i=0; i<numHit; i++) {
			if (!m_olapObjs.ContainsKey (coll [i].gameObject)) {
				m_olapObjs.Add (coll [i].gameObject, null);
				Debug.Log (coll [i].gameObject);
			}
		}
	}
}
