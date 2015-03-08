using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Linq;
//using UnityEditor;

public class loadController : MonoBehaviour
{

		public string m_fileName;
		public GameObject m_player;
		private alertManager alertCtrl;

		public void Start(){
			alertCtrl=gameObject.GetComponent<alertManager>();
		}
		// Use this for initialization
		public void setName (string name)
		{
				m_fileName = name;
		}
		
		public void Awake ()
		{
				DontDestroyOnLoad (transform.gameObject);
		
		}
		bool isHighLevel (GameObject go)
		{
				return (go == go.transform.root.gameObject);
		}
		
		bool meetsRestrict (GameObject go)
		{
				return (isHighLevel (go)) && (go.activeInHierarchy) && (!go.CompareTag ("Player"));
		}
		
		private void handleLiquids (GameObject go)
		{
				liquidController possibleLiq = go.GetComponent<liquidController> ();
				if (possibleLiq == null) {
						return;
				} else {
						possibleLiq.m_spread = false;
						possibleLiq.m_waiting = true;
				}		
		}
		
		void DeleteAll ()
		{
				object[] allObjects = FindObjectsOfType (typeof(GameObject));
				foreach (object thisObject in allObjects) {
						GameObject go = (GameObject)thisObject;
						if (meetsRestrict (go)) {
								handleLiquids (go);
								Destroy (go);
						} 
				}
		}
		
		private Vector2 LoadPosition (StreamReader reader)
		{
				string posLine = reader.ReadLine ();
				string[] lines = posLine.Split (new char[]{','});
				float xPos = float.Parse (lines [0]);
				float yPos = float.Parse (lines [1]);
				reader.ReadLine ();
				return new Vector2 (xPos, yPos);
		}
		private void MovePlayer (Vector2 playerPos)
		{
				gameObject.transform.position = new Vector3 (playerPos.x, playerPos.y, 0);
				//return (GameObject)Instantiate (m_player, new Vector3 (playerPos.x, playerPos.y + 3, 0), Quaternion.identity);
		}
		private string rightPath (string name)
		{
				string[] namesInFolder = {
						"prefabBombPickUp",
						"prefabPickUp",
						"prefabQuartzPickUp",
						"prefabRegolithPickUp",
						"prefabRockPickUp",
						"prefabSuppPickUp",
						"prefabIronPickUp",
						"prefabMagnesiumPickUp",
						"prefabNickelPickUp",
						"prefabArtifactPickUp"
						
				}; 
				if (namesInFolder.Contains (name)) {
						return "PickUps/" + name;
				} else {
						return name;
				}
				
		}
		
		private void SetInventory (GameObject player, StreamReader reader)
		{
				inventoryManager manager = player.GetComponent<inventoryManager> ();
				
				string itemLine = reader.ReadLine ();
				if (itemLine != "") {
						while ((itemLine!=",,")&&(itemLine!="")) {
						
								string[] lines = itemLine.Split (new char[]{','});
								GameObject theItemPrefab = Resources.Load (rightPath (lines [0])) as GameObject;
								Debug.Log ("Trying to make:" + rightPath (lines [0]));
								GameObject theItem = (GameObject)Instantiate (theItemPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
								theItem.name= theItem.name.Remove(theItem.name.Length-7,7);
								
								InvenObject chargeManager = theItem.GetComponent<InvenObject> ();
								chargeManager.setCharges (int.Parse (lines [1]));
																								
								//For some reason, adding theItem spawns a ladder. No idea how this is happening.
								manager.AddItem (theItem);
								theItem.SetActive (false);
								itemLine = reader.ReadLine ();
						}
				}
		}
		private void LoadEnvironment (StreamReader reader)
		{
				string itemLine = reader.ReadLine ();
				if (itemLine != "") {
						while (itemLine!=",,") {
								string[] lines = itemLine.Split (new char[]{','});
								if (lines [0] != "player") {
										GameObject theItemPrefab = Resources.Load (rightPath (lines [0])) as GameObject;
										if (theItemPrefab == null) {
												Debug.Log("failed to prefab: "+rightPath(lines [0]));
										}
										float xPos = float.Parse (lines [1]);
										float yPos = float.Parse (lines [2]);
										GameObject theObj=(GameObject)Instantiate (theItemPrefab, new Vector3 (xPos, yPos, 0), Quaternion.identity);
										//GameObject theObj = (GameObject)PrefabUtility.InstantiatePrefab (theItemPrefab);
										if (theObj!=null){
											theObj.transform.position = new Vector3 (xPos, yPos, 0);
											string oldName=theObj.name;
											//Removes the '(Clone)' added to instantiated objects
											theObj.name= oldName.Remove(oldName.Length-7,7);
										}else{
												Debug.Log("failed to instantiate: "+theItemPrefab);
										}
										
								}
								itemLine = reader.ReadLine ();
								if (itemLine == null) {
										break;
								}
								
						}
				}
		}
		private void LoadPlayer (StreamReader reader)
		{
				Vector2 playerPos = LoadPosition (reader);
				MovePlayer (playerPos);
				SetInventory (gameObject, reader);
		}
		private void LoadResearch (StreamReader reader)
		{
				craftManager manager = gameObject.GetComponent<craftManager> ();
				string firstLine = reader.ReadLine ();
				if (firstLine != "") {
						string[] lines = firstLine.Split (new char[]{','});
						int i = 0;
						foreach (string line in lines) {
								if (line == "0") {
										manager.activate (i);
								}
								i += 1;
						}
						manager.calculateUnlocked ();
				}
				reader.ReadLine ();
		}
		private void LoadMessages (StreamReader reader)
		{
				msgManager manager = gameObject.GetComponent<msgManager> ();
				string firstLine = reader.ReadLine ();
				if (firstLine != "") {
						string[] lines = firstLine.Split (new char[]{','});
						int i = 0;
						foreach (string line in lines) {
								if (line == "0") {
										manager.activate (i);
								}
								i += 1;
						}
						manager.reevalUnLocked ();
				}
				reader.ReadLine ();
				//Debug.Log ("read msgs");
		}
		
		public void LoadAll ()
		{
				DeleteAll ();
				StreamReader reader = new StreamReader (m_fileName, Encoding.Default);
				LoadResearch (reader);
				LoadMessages (reader);
				LoadPlayer (reader);
				LoadEnvironment (reader);
				alertCtrl.setAlert("Loaded File "+m_fileName+" Successfully");
				Debug.Log ("Done Loading");
		}
}
