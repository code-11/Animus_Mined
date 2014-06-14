using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Linq;

public class loadController : MonoBehaviour
{

		public string m_fileName;
		public GameObject m_player;

		// Use this for initialization
		public void setName (string name)
		{
				m_fileName = name;
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
						"prefabSuppPickUp"
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
						while (itemLine!=",,") {
						
								string[] lines = itemLine.Split (new char[]{','});
								GameObject theItemPrefab = Resources.Load (rightPath (lines [0])) as GameObject;
								//Debug.Log (itemLine);
								GameObject theItem = (GameObject)Instantiate (theItemPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
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
								GameObject theItemPrefab = Resources.Load (rightPath (lines [0])) as GameObject;
								GameObject theItem = (GameObject)Instantiate (theItemPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
								itemLine = TextReader.ReadLine ();
						}
				}
		}
		private void LoadPlayer (StreamReader reader)
		{
				Vector2 playerPos = LoadPosition (reader);
				MovePlayer (playerPos);
				SetInventory (gameObject, reader);
		}
		public void LoadAll ()
		{
				StreamReader reader = new StreamReader (m_fileName, Encoding.Default);
				LoadPlayer (reader);
		}
}
