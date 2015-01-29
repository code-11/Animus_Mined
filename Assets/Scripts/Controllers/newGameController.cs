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
			
				//If a feature is special, it can only be spawned once
				private bool m_special;
				private bool m_placed = false;
				private string[,] m_filling;
				public Feature (int sx, int sy, int minX, int maxX, int minY, int maxY, float prob, bool spec)
				{
						//Enter -1 for maximum size for maxX and MaxY
						//size is measured from the top lefthand corner. 
						m_sizeX = sx;
						m_sizeY = sy;
						m_minX = minX;
						m_maxX = maxX;
						m_minY = minY;
						m_maxY = maxY;
						m_prob = prob;
						m_special = spec;
				}
				public void genFilling (string[] possiblePrefabNames)
				{
						string [,] newFilling = new string[m_sizeX, m_sizeY];
						for (int x=0; x<m_sizeX; x+=1) {
								for (int y=0; y<m_sizeY; y+=1) {
										string randStr = possiblePrefabNames [Random.Range (0, possiblePrefabNames.Length)];
										newFilling [x, y] = randStr;
								}
						}
						m_filling = newFilling;
				}
				public void loadFilling (string[,] filling)
				{
						m_filling = filling;
				}
				public void setAsPlaced ()
				{
						m_placed = true;
				}
				public bool isSpecial ()
				{
						return m_special;
				}
				public bool isPlaced ()
				{
						return m_placed;
				}
				public bool shouldGen ()
				{
						int randNum = Random.Range (0, 100);
						return (randNum < m_prob);
				}
				public bool specialAllow ()
				{
						return !(m_special && m_placed);
				}
				public bool withinZone (int x, int y)
				{
						return (((x >= m_minX) && (x <= m_maxX)) && ((y >= m_minY) && (y <= m_maxY)));
				}
				public bool withinWorld (int x, int y, int mapMaxX, int mapMaxY)
				{
						return (x + m_sizeX <= mapMaxX && y + m_sizeY <= mapMaxY);
				}
				public void format (int mapMaxX, int mapMaxY)
				{
						if (m_maxX == -1) {
								m_maxX = mapMaxX - m_sizeX;
						}
						if (m_maxY == -1) {
								m_maxY = mapMaxY - m_sizeY;
						}
/*						Debug.Log ("Feature size set to:" + m_maxX + "," + m_maxY);
*/
				}
				public bool roomForFeature (int x, int y, bool[,] matrix)
				{
						bool toReturn = true;
						for (int curY=y; curY<y+m_sizeY; curY+=1) {
								for (int curX=x; curX<x+m_sizeX; curX+=1) {
										//matrix is true if occupied
										try {
												toReturn &= !matrix [curX, curY];
										} catch {
												Debug.Log ("Tried to access:" + curX + "," + curY);
										}
								}
						}
						return toReturn;
				}
				public void placeInMatrix (int x, int y, bool[,] matrix)
				{
						for (int curY=y; curY<y+m_sizeY; curY+=1) {
								for (int curX=x; curX<x+m_sizeX; curX+=1) {
										matrix [curX, curY] = true;
								}
						}
				}
				public void placeInWorld (int x, int y)
				{
						for (int curY=y; curY<y+m_sizeY; curY+=1) {
								for (int curX=x; curX<x+m_sizeX; curX+=1) {
										placePrefabInWorld (m_filling [curX - x, curY - y], curX, curY);
/*										GameObject thePrefab = Resources.Load ("prefabPackedRegolith") as GameObject;
										Instantiate (thePrefab, new Vector3 (curX, curY, 0), Quaternion.identity);*/
								}
						}
						//Debug.Log ("place feature here");
				}
		}
		private string m_fileName;
		private ArrayList features = new ArrayList (); 
		private static int m_endX = 20;
		private static int m_endY = 20;
		//In world coordinates
		private static int m_startX = 6;
		private static int m_startY = -9;
		//The generation coordinates are 'display' cordinates meaning that (0,0) is top left
		//No idea why I did that
		private bool[,] filledMatrix = new bool[m_endX, m_endY];
		//private Hashtable m_percentTable;
		private static int flipY (int y)
		{
				return - y;
		}
		private static void placePrefabInWorld (string name, int x, int y)
		{
				if (name != "") {
						GameObject thePrefab = Resources.Load (name) as GameObject;
						Instantiate (thePrefab, new Vector3 (x, flipY (y), 0), Quaternion.identity);
				}
		}
		private void movePlayerToStart (int x, int y)
		{
				GameObject player = GameObject.FindGameObjectWithTag ("Player");
				player.transform.position = new Vector3 (x, y, 0);
		}
		private void createTest ()
		{
				string k = "prefabKamacite";
				string p = "prefabPackedRegolith";
				string q = "prefabQuartz";
				string r = "prefabRock";
				string g = "prefabRegolith";
				string t = "prefabTaenite";
				string i = "prefabInvBlk";
				string w = "prefabWater";
				string f = "prefabFactory";
				string l = "PickUps/prefabPickUp";
				string a = "";
				
				int center = m_endX / 2;
				Debug.Log (center - 4);
		
				//Trick to thinking about the filling: rotate all subarrays clockwise and put together in order
				Feature surface = new Feature (m_endX, 3, 0, 0, 0, 0, 99, true);
				surface.genFilling (new string[] {p,g});
				features.Add (surface);
				
				Feature startingArea = new Feature (7, 6, 5, 5, 5, 5, 99, true);
				startingArea.loadFilling (new string[,]{
					{r,r,p,p,k,p},
					{r,p,a,a,a,r},
					{g,p,a,a,l,r},
					{p,p,a,a,f,p},
					{r,a,a,r,r,p},
					{r,a,a,a,g,r},
					{r,p,p,g,g,r}
				});
				features.Add (startingArea);
				
/*				Feature grotto = new Feature (4, 3, 1, 15, 0, 17, 1, false);
				grotto.loadFilling (new string[,]{{a,w,p},{a,w,g},{a,w,g},{a,w,g}});
				features.Add (grotto);*/
				
		}
		private int printHelp (bool inB)
		{
				if (inB) {
						return 1;
				} else {
						return 0;
				}
		}
		private void printMatrix (bool[,] matrix)
		{
				int maxX = matrix.GetLength (0);
				int maxY = matrix.GetLength (1);
				for (int tempY=0; tempY<maxY; tempY+=1) {
						string str = "[" + tempY + ":";
						for (int tempX=0; tempX<maxX; tempX+=1) {
								str += printHelp (matrix [tempX, tempY]) + ",";
						}
						str += "]";
						Debug.Log (str);
				}
		}
		private void evalFeature (int x, int y, bool[,] matrix, Feature feat)
		{
/*				Debug.Log ("Tried to place feature");
*/
				if (feat.specialAllow ()) {
						if (feat.withinWorld (x, y, m_endX, m_endY)) {
								if (feat.withinZone (x, y)) {
										if (feat.shouldGen () && feat.roomForFeature (x, y, matrix)) {
												feat.placeInMatrix (x, y, matrix);
												feat.placeInWorld (x, y);
												feat.setAsPlaced ();
												Debug.Log ("feature placed at:" + x + "," + y);
										}
								}
						}
				}
/*				if (feat.isSpecial ()) {
						if (!(feat.withinWorld (x, y, m_endX, m_endY))) {
								Debug.Log ("Failed due to world size restrictions");
						} else if (!(feat.withinZone (x, y))) {
								Debug.Log ("Failed due to spawn zone restrictions");		
						} else if (!(feat.shouldGen ())) {
								Debug.Log ("Failed due to probability");
						} else if (!(feat.roomForFeature (x, y, matrix))) {
								Debug.Log ("Failed due to room restrictions");
						}
				}*/
				
			
		}
		private void setupFeatures ()
		{
				foreach (Feature feat in features) {
						for (int y=0; y< filledMatrix.GetLength(1); y+=1) {
								for (int x=0; x<filledMatrix.GetLength(0); x+=1) {
										evalFeature (x, y, filledMatrix, feat);
								}
								//Debug.Log ("Features would be placed here" + x);
						}
						if ((feat.isSpecial ()) && (!feat.isPlaced ())) {
								Debug.Log ("Unable to place a special feature");
						}
				}

		
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
						return "prefabPackedRegolith";
				}
		}
		public void fillRest ()
		{
				for (int y=0; y<m_endY; y+=1) {
						for (int x=0; x<m_endX; x+=1) {
								int randNum = Random.Range (0, 145);
								string prefabName = choose (randNum);
								if ((prefabName != "") && (filledMatrix [x, y] == false)) {
										placePrefabInWorld (prefabName, x, y);
/*					
										GameObject thePrefab = Resources.Load (prefabName) as GameObject;
										Instantiate (thePrefab, new Vector3 (x, y, 0), Quaternion.identity);*/
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
				createTest ();
				setupFeatures ();
				fillRest ();
				movePlayerToStart (m_startX, m_startY);
				printMatrix (filledMatrix);
		}
}
