using UnityEngine;
using System.Collections;

public class InvenObject : MonoBehaviour
{
		public int m_charges = 0; 
	
		public int getCharges ()
		{
				return m_charges;
		}
		public void setCharges (int newCharges)
		{
				m_charges = newCharges;
		}
		public void decCharges ()
		{
				m_charges -= 1;
		}
	
}
