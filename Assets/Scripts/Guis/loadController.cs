using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

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
		private GameObject MakePlayer (Vector2 playerPos)
		{
				return (GameObject)Instantiate (m_player, new Vector3 (playerPos.x, playerPos.y + 3, 0), Quaternion.identity);
		}
		private void SetInventory (GameObject player, StreamReader reader)
		{
				inventoryManager manager = player.GetComponent<inventoryManager> ();
			
				string itemLine = reader.ReadLine ();
				if (itemLine != "") {
						string[] lines = itemLine.Split (new char[]{','});
						GameObject theItemPrefab = Resources.Load (lines [0]) as GameObject;
						GameObject theItem = (GameObject)Instantiate (theItemPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
						//Not sure that 0,0,0 was right place to put objects... what state are they actually in?
				}
		}
		private int LoadPlayer (StreamReader reader)
		{
				Vector2 playerPos = LoadPosition (reader);
				//GameObject player = MakePlayer (playerPos);
				SetInventory (gameObject, reader);
				return 0;
		}
		public void LoadAll ()
		{
				StreamReader reader = new StreamReader (m_fileName, Encoding.Default);
				LoadPlayer (reader);
		}
}
