using UnityEngine;
using System.Collections;

public class InvenObject : MonoBehaviour
{
		public int m_charges = 0; 
		public string m_stackName;
		
		public void setStackName (string name)
		{
				m_stackName = name;
		}
		public string getStackName ()
		{
				return m_stackName;
		}
		public int getCharges ()
		{
				return m_charges;
		}
		public void setCharges (int newCharges)
		{
				m_charges = newCharges;
		}
		public void incrCharges (int charges)
		{
				m_charges += charges;
		}
		public void decCharges ()
		{
				m_charges -= 1;
		}
	
}
