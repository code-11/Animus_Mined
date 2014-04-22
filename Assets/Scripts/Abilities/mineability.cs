using UnityEngine;
using System.Collections;



public class mineability : MonoBehaviour
{
		public GameObject m_drop;
		public int m_numDrop;
		public int m_health = 100;

		void Update ()
		{
				if (m_health <= 0) {
						if (m_drop != null) {
								GameObject theDrop = (GameObject)Instantiate (m_drop, new Vector3 (transform.position.x, transform.position.y, 0), Quaternion.identity);
								InvenObject inven = theDrop.GetComponent<InvenObject> ();
								inven.setCharges (m_numDrop);
						}
						Destroy (this.gameObject);
				}
		}
		public int GetHealth ()
		{
				return m_health;
		}
		public void SetHealth (int newHealth)
		{
				m_health = newHealth;
		}
}
