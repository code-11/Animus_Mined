using UnityEngine;
using System.Collections;

public class liquidController : MonoBehaviour
{
		public GameObject m_toSpread;
		public bool m_spread = false;
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
	
		}
		// Update is called once per frame
		void Update ()
		{
				spread ();
		}
		
		void spread ()
		{
				foreach (int dir in m_dirs.GetValues(typeof(m_dirs))) {
						if (dir == (int)m_dirs.down) {
								spreadPoint (0, -1);
						} else if (dir == (int)m_dirs.left) {
								spreadPoint (-1, 0);
						} else if (dir == (int)m_dirs.right) {
								spreadPoint (1, 0);
						}
				}
		}
		
		void spreadPoint (int devX, int devY)
		{
				Vector2 newPos = new Vector2 (transform.position.x + devX, transform.position.y + devY);
				Physics2D.OverlapPointNonAlloc (newPos, hits);
				if ((m_spread) && (hits [0] == null)) {
						GameObject spawn = (GameObject)Instantiate (m_toSpread, new Vector3 (newPos.x, newPos.y, 0), Quaternion.identity);
						liquidController brain = spawn.GetComponent<liquidController> ();
						brain.m_toSpread = m_toSpread;
						brain.m_spread = true;	
				} 
				
				hits [0] = null;
		}
}
