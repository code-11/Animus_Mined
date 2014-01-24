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
		private bool invenUp = false;
		public int selected = 0;
		
		public GameObject getSelObj ()
		{
				return (GameObject)m_inventory [selected];
		}
		public void setSelNum (int newNum)
		{
				selected = newNum;
		}
		public int getSelNum ()
		{
				return selected;
		}
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
		
		void shiftLeft ()
		{
				if (selected > 0) {
						selected -= 1;
				}
		}
		void shiftRight ()
		{
				if (selected < invenNum) {
						selected += 1;
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
						
						m_inventory.Add (invenNum, item);
						invenNum += 1;
				}
		}
		public void RemoveSelItem ()
		{
				m_inventory.Remove (selected);
				for (int index=selected; index<invenNum; index++) {
						if (m_inventory.ContainsKey (index + 1)) {
								Debug.Log ("Replacing slot " + index + " with item at " + (index + 1));
								m_inventory.Add (index, m_inventory [index + 1]);
								m_inventory.Remove (index + 1);
						}
				}
				selected = 0;
				//Destroy()
		}
		public void UseSelected ()
		{
				Usability useScript = getSelObj ().GetComponent<Usability> (); 
				if (useScript != null) {
						useScript.Use ();
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