using UnityEngine;
using System.Collections;
using System.IO;
//using UnityEditor;

public class saveController : MonoBehaviour
{
		public string m_saveName;
 
		
		// Use this for initialization
		void Start ()
		{
				//SaveAll ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		public void setName (string filepath)
		{
				m_saveName = filepath;
		}
		/*
		bool isPrefab (GameObject go)
		{
				return (PrefabUtility.GetPrefabParent (go)) != null;
		}
		string findPrefabName (GameObject go)
		{
				return ((GameObject)PrefabUtility.GetPrefabParent (go)).name;
		}
		*/
		bool isHighLevel (GameObject go)
		{
				return (go == go.transform.root.gameObject);
		}
		/*bool meetsRestrict (GameObject go)
		{
				return (isHighLevel (go)) && (isPrefab (go)) && (go.activeInHierarchy);
		}*/
		bool meetsLaxRestrict(GameObject go){
				return (isHighLevel (go)) && (go.activeInHierarchy);
		}
		int getCharges (GameObject go)
		{
				InvenObject item = go.GetComponent<InvenObject> ();
				if (item == null) {
						Debug.Log ("Item in inventory is not an Inven object!");
						return -1;
				} else {
						return item.getCharges ();
				}
		}

		void SaveResearch (StreamWriter strWrite)
		{
				craftManager manager = gameObject.GetComponent<craftManager> ();
				if (manager != null) {
						foreach (craftManager.Recipe recipe in manager.getAllRecipesIncludingLock()) {
								if (recipe.isLocked ()) {
										strWrite.Write ("1,");
								} else {
										strWrite.Write ("0,");
								}
						}
						strWrite.Write ("\n\n");
						
				} else {
						Debug.Log ("Player has no craft manager");
				}
		}

		void SaveMessages (StreamWriter strWrite)
		{
				msgManager manager = gameObject.GetComponent<msgManager> ();
				if (manager != null) {
						foreach (msgManager.Message msg in manager.getAllMessagesIncludingLock()) {
								if (msg.isLocked ()) {
										strWrite.Write ("1,");
								} else {
										strWrite.Write ("0,");
								}
						}
						strWrite.Write ("\n\n");
				} else {
						Debug.Log ("Player has no message manager");
				}
		}

		void SavePlayer (StreamWriter strWrite)
		{
				strWrite.WriteLine (transform.position.x + "," + transform.position.y);
				strWrite.WriteLine ("");
				inventoryManager manager = gameObject.GetComponent<inventoryManager> ();
				if (manager != null) {
						ArrayList inven = manager.getInven ();
						if (inven == null) {
								inven = new ArrayList ();
						}
						foreach (GameObject obj in inven) {
										int charges = getCharges (obj);
										strWrite.WriteLine (obj.name + "," + charges);
						}
				}
				strWrite.WriteLine ("");
		}
		void SaveLevel (StreamWriter strWrite)
		{
				object[] allObjects = FindObjectsOfType (typeof(GameObject));
				foreach (object thisObject in allObjects) {
						GameObject go = (GameObject)thisObject;
						if(meetsLaxRestrict(go)){
								float tempX = go.transform.position.x;
								float tempY = go.transform.position.y;
								//Debug.Log (isHighLevel (go) + "," + go + "," + (go == isHighLevel (go)));
								strWrite.WriteLine (go.name + "," + tempX + "," + tempY);

						} else {
								Debug.Log (go.name + " Does not meet saving restrictions");
						}
				}
		}
		public void SaveAll ()
		{
				alertManager alertCtrl=gameObject.GetComponent<alertManager>();

				try{
					StreamWriter strWrite = File.CreateText (m_saveName); 
					SaveResearch (strWrite);
					SaveMessages (strWrite);
					SavePlayer (strWrite);
					SaveLevel (strWrite);
					strWrite.Close ();
					alertCtrl.setAlert("Save Game Successful");
				}catch{
					alertCtrl.setAlert("Save Game Failed");
				}
		}
}
