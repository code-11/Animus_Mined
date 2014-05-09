using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

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
		
		bool isPrefab (GameObject go)
		{
				return (PrefabUtility.GetPrefabParent (go)) != null;
		}
		string findPrefabName (GameObject go)
		{
				return ((GameObject)PrefabUtility.GetPrefabParent (go)).name;
		}
		bool isHighLevel (GameObject go)
		{
				return (go == go.transform.root.gameObject);
		}
		bool meetsRestrict (GameObject go)
		{
				return (isHighLevel (go)) && (isPrefab (go)) && (go.activeInHierarchy);
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

		void SavePlayer (StreamWriter strWrite)
		{
				inventoryManager manager = gameObject.GetComponent<inventoryManager> ();
				if (manager != null) {
						ArrayList inven = manager.getInven ();
						if (inven == null) {
								inven = new ArrayList ();
						}
						foreach (GameObject obj in inven) {
								if (isPrefab (obj)) {
										string preName = findPrefabName (obj);
										int charges = getCharges (obj);
										strWrite.WriteLine (preName + "," + charges);
								}
						}
				}
				strWrite.WriteLine ("");
		}
		void SaveLevel (StreamWriter strWrite)
		{
				object[] allObjects = FindObjectsOfType (typeof(GameObject));
				foreach (object thisObject in allObjects) {
						GameObject go = (GameObject)thisObject;
						if (meetsRestrict (go)) {
								float tempX = go.transform.position.x;
								float tempY = go.transform.position.y;
								//Debug.Log (isHighLevel (go) + "," + go + "," + (go == isHighLevel (go)));
								strWrite.WriteLine (findPrefabName (go) + "," + tempX + "," + tempY);
						}
				}
		}
		public void SaveAll ()
		{
				StreamWriter strWrite = File.CreateText (m_saveName); 
				SavePlayer (strWrite);
				SaveLevel (strWrite);
				strWrite.Close ();
		}
}
