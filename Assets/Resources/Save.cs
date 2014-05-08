using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

public class Save : MonoBehaviour
{
		public string m_saveName;
		private StreamWriter  m_fileWriter; 
		// Use this for initialization
		void Start ()
		{
				SaveLevel ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
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
		
		void SaveLevel ()
		{
				m_fileWriter = File.CreateText (m_saveName);
				object[] allObjects = FindObjectsOfType (typeof(GameObject));
				foreach (object thisObject in allObjects) {
						GameObject go = (GameObject)thisObject;
						if (meetsRestrict (go)) {
								float tempX = go.transform.position.x;
								float tempY = go.transform.position.y;
								//Debug.Log (isHighLevel (go) + "," + go + "," + (go == isHighLevel (go)));
								m_fileWriter.WriteLine (findPrefabName (go) + "," + tempX + "," + tempY);
						}
				}
				m_fileWriter.Close ();
		}
}
