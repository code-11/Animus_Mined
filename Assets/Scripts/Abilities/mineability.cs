using UnityEngine;
using System.Collections;

public class mineability : MonoBehaviour
{
		public int m_health = 100;

		void Update ()
		{
				if (m_health <= 0) {
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
