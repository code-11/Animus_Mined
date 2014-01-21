using UnityEngine;
using System.Collections;

public class inventoryManager : MonoBehaviour
{

		/*
		m_inventory is a hashtable that remembers items in the player's inventory
		Key: inventory number, ie which slot it is in.
		Value: the gameobject itself.
		*/
		private Hashtable m_inventory;
		//public GameObject m_placeholder;
		private int invenNum = 0;
		private bool allowI = true;
		
		void Start ()
		{
/*				GameObject m_startItems[]={};*/
				m_inventory = new Hashtable ();
/*				foreach (var item in m_startItems) {
						m_inventory.Add (m_inventory.Count, item);
				}*/
		}
		void Update ()
		{
				runInven ();
		}
		
		void runInven ()
		{

				bool iPress = Input.GetKey ("i");
				if ((iPress) && (allowI)) {
						allowI = false;
						StartCoroutine (viewIven ());
				}
				pickUp ();				
		}	
		public Hashtable getInven ()
		{
				return m_inventory;
		}
		void pickUp ()
		{
				overlapDetector waitItems = gameObject.GetComponent<overlapDetector> ();
				if (waitItems != null) {
						if (waitItems.m_olapObjs.ContainsKey ("PickUp")) {
								GameObject theObj = (GameObject)waitItems.m_olapObjs ["PickUp"];
								AddItem (theObj);
								theObj.SetActive (false);
								waitItems.m_olapObjs.Remove ("PickUp");
						}
				}
		}
		public void AddItem (GameObject item)
		{
				if (!m_inventory.ContainsValue (item)) {
						invenNum += 1;
						m_inventory.Add (invenNum, item);
				}
		}
		IEnumerator viewIven ()
		{
				yield return new WaitForSeconds (0.2f);
				allowI = true;
				foreach (var item in m_inventory.Values) {
						Debug.Log (item);
				}
		}
		
}