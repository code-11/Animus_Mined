using UnityEngine;
using System.Collections;

public class inventoryManager : MonoBehaviour
{

		/*
		m_inventory is a hashtable that remembers items in the player's inventory
		Key: inventory number, ie which slot it is in.
		Value: the gameobject itself.
		*/
		private ArrayList m_inventory;
		//public GameObject m_placeholder;
		private bool invenUp = false;
		private int m_numSelected = 0;
		
		public GameObject getSelObj ()
		{
				if (m_inventory.Count > 0) {
						return (GameObject)m_inventory [m_numSelected];
				} else {
						return null;
				}
		}
		public void setSelNum (int newNum)
		{
				m_numSelected = newNum;
		}
		public int getSelNum ()
		{
				return m_numSelected;
		}
		void Start ()
		{
/*				GameObject m_startItems[]={};*/
				m_inventory = new ArrayList ();
/*				foreach (var item in m_startItems) {
						m_inventory.Add (m_inventory.Count, item);
				}*/
		}
		void Update ()
		{
				runInven ();
		}
		
		void shiftLeft ()
		{
				if (m_numSelected == 0) {
						m_numSelected = 0;
				} else {
						m_numSelected -= 1;
				}
		}
		void shiftRight ()
		{
				if (m_numSelected < m_inventory.Count - 1) {
						m_numSelected += 1;
				}
		}
		
		void runInven ()
		{
				InventoryGui invenGui = gameObject.GetComponent<InventoryGui> ();
				bool iPress = Input.GetKeyDown ("i");
				bool selLeft = Input.GetKeyDown ("q");
				bool selRight = Input.GetKeyDown ("e");
				bool rPress = Input.GetKeyDown ("r");
				bool pPress = Input.GetKeyDown ("p");
				if (pPress) {
						RemoveSelItem ();
				}
				if (rPress) {
						UseSelected ();
				}
				if (selLeft) {
						shiftLeft ();
				}
				if (selRight) {
						shiftRight ();
				}
				if (iPress) {
						if (invenUp == true) {
								invenUp = false;
								invenGui.enabled = false;
						} else {
								invenUp = true;
								invenGui.enabled = true;
						}
						//StartCoroutine (viewIven ());
				}
				pickUp ();				
		}	
		public ArrayList getInven ()
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
				if (!m_inventory.Contains (item)) {
						InvenObject chargeScript = item.GetComponent<InvenObject> ();
						string name = chargeScript.getStackName ();
						foreach (GameObject obj in m_inventory) {
								InvenObject objChargeScript = obj.GetComponent<InvenObject> ();
								if (objChargeScript.getStackName () == name) {
										objChargeScript.incrCharges (chargeScript.getCharges ());
										return;
								}
						}
						m_inventory.Add (item);
				}
		}
		public void RemoveSelItem ()
		{
				m_inventory.RemoveAt (m_numSelected);
/*		for (int index=selected; index<invenNum; index++) {
			if (m_inventory.ContainsKey (index + 1)) {
				//Debug.Log ("Replacing slot " + index + " with item at " + (index + 1));
				m_inventory.Add (index, m_inventory [index + 1]);
				m_inventory.Remove (index + 1);
			}
		}*/
				m_numSelected = 0;
				//Destroy()
		}
		public void UseSelected ()
		{
				if (getSelObj () != null) {
						Usability useScript = getSelObj ().GetComponent<Usability> (); 
						InvenObject chargeScript = getSelObj ().GetComponent<InvenObject> ();
						if ((useScript != null) && (chargeScript != null)) {
								if (useScript.Use (gameObject.transform))
										chargeScript.decCharges ();
								if (chargeScript.getCharges () <= 0) {
										RemoveSelItem ();
								}
						}
				}
		}
/*		IEnumerator viewIven ()
		{
				yield return new WaitForSeconds (0.2f);
				allowI = true;
				foreach (var item in m_inventory.Values) {
						Debug.Log (item);
				}
		}*/
		
}