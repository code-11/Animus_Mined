using UnityEngine;
using System.Collections;



public class mineability : MonoBehaviour
{
		public GameObject m_drop;
		public int m_numDrop;
		public int m_health = 100;

		private int m_maxHealth;
		private SpriteRenderer rend;

		public int m_percentChance = 100;

		public Sprite m_damaged1;
		public Sprite m_damaged2;

		void Start(){
			m_maxHealth=m_health;
			rend=gameObject.GetComponent<SpriteRenderer>();
		}

		bool doesDrop ()
		{
				int rand = Random.Range (0, 100);
				return (rand < m_percentChance);
		}

		void Update ()
		{
				if (m_health <= 0) {
						if ((m_drop != null) && (doesDrop ())) {
								GameObject theDrop = (GameObject)Instantiate (m_drop, new Vector3 (transform.position.x, transform.position.y, 0), Quaternion.identity);
								theDrop.name= theDrop.name.Remove(theDrop.name.Length-7,7);
								InvenObject inven = theDrop.GetComponent<InvenObject> ();
								inven.setCharges (m_numDrop);
						}
						Destroy (this.gameObject);
				}else if((m_health>=(m_maxHealth*1/3))&&(m_health<(m_maxHealth*2/3))){
					rend.sprite=m_damaged1;
				}else if ((m_health<(m_maxHealth*1/3))&&(m_health>0)){
					rend.sprite=m_damaged2;
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
