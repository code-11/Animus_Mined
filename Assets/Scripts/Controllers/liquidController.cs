using UnityEngine;
using System.Collections;

public class liquidController : MonoBehaviour
{
		public GameObject m_toSpread;
		public bool m_spread = false;
		public float m_spreadDelaySlow;
		public float m_spreadDelayFast;
		public bool m_checkSpeed;
		public bool m_waiting = false;
		private enum m_dirs
		{
				up,
				down,
				left,
				right}
		;
		private Collider2D[] hits = new Collider2D[1];
		// Use this for initialization
		void Start ()
		{
				m_waiting = false;
		}
		// Update is called once per frame
		void Update ()
		{
				//Debug.Log (m_waiting);
				//if (!m_waiting)
				if (!m_waiting) {
						StartCoroutine (spreadTimeFast ());
				}
		}
		
		IEnumerator spreadTimeFast ()
		{
				m_waiting = true;
				yield return new WaitForSeconds (m_spreadDelayFast);
				spread ();
				m_waiting = false;
		}
		
		void spread ()
		{
				//Debug.Log ("checking");
				bool anySpread = false;
				foreach (int dir in m_dirs.GetValues(typeof(m_dirs))) {
						if (dir == (int)m_dirs.down) {
								anySpread = anySpread || spreadPoint (0, -1);
						} else if (dir == (int)m_dirs.left) {
								anySpread = anySpread || spreadPoint (-1, 0);
						} else if (dir == (int)m_dirs.right) {
								anySpread = anySpread || spreadPoint (1, 0);
						}
				}
		}
		
		bool spreadPoint (int devX, int devY)
		{
				bool toReturn;
				Vector2 newPos = new Vector2 (transform.position.x + devX, transform.position.y + devY);
				Physics2D.OverlapPointNonAlloc (newPos, hits);
				if ((m_spread) && (hits [0] == null)) {
						try {
								GameObject spawn = (GameObject)Instantiate (m_toSpread, new Vector3 (newPos.x, newPos.y, 0), Quaternion.identity);
					
								liquidController brain = spawn.GetComponent<liquidController> ();
								brain.m_toSpread = m_toSpread;
								brain.m_spread = true;	
								toReturn = true;
						} catch (MissingReferenceException) {
								toReturn = false;
								Destroy (gameObject);
						}
				} else {
						toReturn = false;
				}
				
				hits [0] = null;
				return toReturn;
		}
}
