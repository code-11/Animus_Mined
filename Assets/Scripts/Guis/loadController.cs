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
						"prefabSupportPickUp"
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
						string[] lines = itemLine.Split (new char[]{','});
						GameObject theItemPrefab = Resources.Load (rightPath (lines [0])) as GameObject;
						GameObject theItem = (GameObject)Instantiate (theItemPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
						//For some reason, adding theItem spawns a ladder. No idea how this is happening.
						//manager.AddItem (theItem);
						//theItem.SetActive (false);
						Debug.Log (lines [0]);
				}
		}
		private int LoadPlayer (StreamReader reader)
		{
				Vector2 playerPos = LoadPosition (reader);
				MovePlayer (playerPos);
				SetInventory (gameObject, reader);
				return 0;
		}
		public void LoadAll ()
		{
				StreamReader reader = new StreamReader (m_fileName, Encoding.Default);
				LoadPlayer (reader);
		}
}
