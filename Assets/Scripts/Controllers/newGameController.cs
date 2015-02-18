using UnityEngine;
using System.Collections;
using UnityEditor;

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
				private string m_name;
				private bool m_placed = false;
				private string[,] m_filling;
				public Feature (int sx, int sy, int minX, int maxX, int minY, int maxY, float prob, bool spec, string name)
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
						m_name = name;
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
				public string getName ()
				{
						return m_name;
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
						int randNum = Random.Range (0, 1000);
						return (randNum < m_prob);
				}
				public bool specialAllow ()
				{
						return !(m_special && m_placed);
				}
				public bool withinZone (int x, int y)
				{
						bool withinX = ((x >= m_minX) && (x <= m_maxX));
						bool withinY = ((y >= m_minY) && (y <= m_maxY));
						return (withinX && withinY);
/*						if (withinX && withinY) {
								return true;
						} else {
								if (!withinX) { 
										Debug.Log ("x" + x + " minX:" + m_minX + " maxX:" + m_maxX + " Failed within Zone due to x restrict");
								}
								return false;
						}*/
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
						if (m_maxX == -2) {
								m_maxX = mapMaxX - m_sizeX - 1;
						}
						if (m_maxY == -2) {
								m_maxY = mapMaxY - m_sizeY - 1;
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
		public static int m_endX = 40;
		public static int m_endY = 80;
		public static int m_surfaceSize = 3;
		//In world coordinates
		private static int m_startX = (m_endX / 2) - 3;
		private static int m_startY = -(m_surfaceSize + 6);
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
						if (name != "prefabHypersthene") {
								GameObject thePrefab = Resources.Load (name) as GameObject;
								GameObject theObj = (GameObject)PrefabUtility.InstantiatePrefab (thePrefab);
								theObj.transform.position = new Vector3 (x, flipY (y), 0);
						} else {
								GameObject thePrefab = Resources.Load (name) as GameObject;
								float fixedX = x + .5f;
								float fixedY = flipY (y) - .5f;
								GameObject theObj = (GameObject)PrefabUtility.InstantiatePrefab (thePrefab);
								theObj.transform.position = new Vector3 (fixedX, fixedY, 0);
						}
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
				string h = "prefabHypersthene";
				string d = "prefabFeldspar";
				
				string i = "prefabInvBlk";
				string w = "prefabWater";
				string f = "prefabFactory";
				
				string l = "PickUps/prefabPickUp";
				string a = "";
				
				
				//Debug.Log (center - 4);
		
				//Trick to thinking about the filling: rotate all subarrays clockwise and put together in order
				Feature surface = new Feature (m_endX, m_surfaceSize, 0, 0, 0, 0, 1000, true, "Surface");
				surface.genFilling (new string[] {p,g});
				features.Add (surface);
				
				Feature end = new Feature (m_endX, 1, 0, 0, m_endY - 1, m_endY - 1, 1000, true, "End");
				end.genFilling (new string[]{i});
				features.Add (end);
				
				int wldstrtY = flipY (m_startY);
				Feature startingArea = new Feature (7, 6, m_startX - 1, m_startX - 1, wldstrtY - 4, wldstrtY - 4, 1000, true, "StartingArea");
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
				
				Feature grotto = new Feature (4, 3, 1, m_endX - 4 - 1, m_surfaceSize, m_endY - 3 - 1, 5, false, "Grotto");
				grotto.loadFilling (new string[,]{{a,w,p},{a,w,g},{a,w,g},{a,w,g}});
				features.Add (grotto);
				
				Feature quartz = new Feature (3, 3, 1, -1, m_surfaceSize + (m_endY / 2), -1, 20, false, "Quartz");
				quartz.format (m_endX, m_endY);
				quartz.loadFilling (new string[,]{
					{a,d,r},
					{d,q,d},
					{p,d,g}
				});
				features.Add (quartz);
				
				//Feature (int sx, int sy, int minX, int maxX, int minY, int maxY, float prob, bool spec, string name)
			
				
				Feature hyper = new Feature (2, 2, 1, -1, m_surfaceSize, -2, 20, false, "Hyper");
				hyper.format (m_endX, m_endY);
				hyper.loadFilling (new string[,]{{h,a},{a,a,}});
				features.Add (hyper);
				
				
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
				Feature leftBound = new Feature (1, m_endY, -1, -1, 0, 0, 1000, true, "LeftBound");
				leftBound.genFilling (new string[]{"prefabInvBlk"});
				leftBound.placeInWorld (-1, 0);
				
				Feature rightBound = new Feature (1, m_endY, m_endX, m_endX, 0, 0, 1000, true, "RightBound");
				rightBound.genFilling (new string[]{"prefabInvBlk"});
				rightBound.placeInWorld (m_endX, 0);
				//evalFeature (-1, 0, filledMatrix, leftBound);
		
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
						return "prefabPackedRegolith";
				} else if (betwn (65, 105, scaledNum)) {
						return "prefabRegolith";
				} else if (betwn (105, 135, scaledNum)) {
						return "prefabRock";
				} else if (betwn (135, 145, scaledNum)) {
						return "prefabTaenite";
				} else if (betwn (145, 155, scaledNum)) {
						return "";
				} else {
						return "";
				}
		}
		public void fillRest ()
		{
				for (int y=0; y<m_endY; y+=1) {
						for (int x=0; x<m_endX; x+=1) {
								int randNum = Random.Range (0, 155);
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
				//printMatrix (filledMatrix);
		}
}
