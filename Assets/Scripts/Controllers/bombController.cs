using UnityEngine;
using System.Collections;

public class bombController : MonoBehaviour
{

		public float m_explosionSize;
		public void setExplosionSize (float size)
		{
				m_explosionSize = size;
		}
		// Use this for initialization
		void Start ()
		{
				StartCoroutine (explodeInTime ());
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		IEnumerator explodeInTime ()
		{
				yield return new WaitForSeconds (2f);
				explode (gameObject.transform);
		}
		public void explode (Transform pos)
		{
				Collider2D [] hits;
				hits = Physics2D.OverlapCircleAll (pos.position, m_explosionSize);
				foreach (Collider2D hit in hits) {
						mineability mineObj = hit.gameObject.GetComponent<mineability> ();
						if (mineObj != null) {
								mineObj.SetHealth (0);
						}
				}
				Destroy (gameObject);
		}
}
