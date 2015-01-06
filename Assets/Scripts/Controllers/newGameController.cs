using UnityEngine;
using System.Collections;

public class newGameController : MonoBehaviour
{
		private class Feature : Object
		{
				//The size of the feature in grid blocks
				private int m_sizeX;
				private int m_sizeY;
			
				//Where the feature can spawn
				//-1 indicates thatis goes on forever
				private int m_minX;
				private int m_minY;
				private int m_maxX;
				private int m_maxY;
			
				//what is the probablility that the feature will spawn
				private float m_prob;
			
				//If a featuer is special, it can only be spawned once
				private bool m_special;
				private bool m_placed = false;
				public Feature (int sx, int sy, int minX, int maxX, int minY, int maxY, float prob, bool spec)
				{
						m_sizeX = sx;
						m_sizeY = sy;
						m_minX = minX;
						m_maxX = maxX;
						m_minY = minY;
						m_maxY = maxY;
						m_prob = prob;
						m_special = spec;
				}
				public void place ()
				{
						Debug.Log ("place feature here");
				}
		}
		private string m_fileName;
		//private Arraylist features = new ArrayList (); 
		//private Hashtable m_percentTable;
		private int m_endX = 20;
		private int m_endY = 20;
		private void setupFeatures ()
		{
				Debug.Log ("Features would be placed here");
		
		} 	
		private bool betwn (int low, int high, int num)
		{
				return (num >= low) && (num < high);
		}
		private string choose (int randNum)
		{
				//When added all up, percents are 90% meaning 10% air(gap)
/*				m_percentTable = new Hashtable ();                //From 0-1   //CumSum (*100)
				m_percentTable.add (.1, "prefabKamacite");        //.1         //10
				m_percentTable.add (.5, "prefabPackedRegolith");  //.5         //60
				m_percentTable.add (.05, "prefabQuartz");         //.05		   //65
				m_percentTable.add (.4, "prefabRegolith");        //.4		   //105
				m_percentTable.add (.3, "prefabRock");            //.3         //135
				m_percentTable.add (.1, "prefabTaenite");         //.1		   //145*/
				int scaledNum = randNum;
				if (betwn (0, 10, scaledNum)) {
						return "prefabKamacite";
				} else if (betwn (10, 60, scaledNum)) {
						return "prefabPackedRegolith";
				} else if (betwn (60, 65, scaledNum)) {
						return "prefabQuartz";
				} else if (betwn (65, 105, scaledNum)) {
						return "prefabRegolith";
				} else if (betwn (105, 135, scaledNum)) {
						return "prefabRock";
				} else if (betwn (135, 145, scaledNum)) {
						return "prefabTaenite";
				} else if (scaledNum == 145) {
						return "prefabTaenite";
				} else {
						return "";
				}
		}
		public void fillRest ()
		{
				for (int y=0; y<m_endY; y+=1) {
						for (int x=0; x<m_endX; x+=1) {
								int randNum = Random.Range (0, 145);
								string prefabName = choose (randNum);
								if (prefabName != "") {
										GameObject thePrefab = Resources.Load (prefabName) as GameObject;
										Instantiate (thePrefab, new Vector3 (x, y, 0), Quaternion.identity);
								}
						}
				}
		}
		public void setName (string name)
		{
				m_fileName = name;
		}

		public void makeNewGame ()
		{
				fillRest ();
		}
}
