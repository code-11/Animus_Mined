using UnityEngine;
using System.Collections;
using UnityEditor;

public class startingManager : escManager
{

		public override void addAllItems ()
		{
				m_menuList = new ArrayList ();	
				menuItem item1 = new menuItem ("New Game", newGame);
				menuItem item2 = new menuItem ("Load Game", loadFile);
				m_menuList.Add (item1);
				m_menuList.Add (item2);
				m_numSelected = 0;
		}
	
		private void newGame ()
		{
				string filePath = EditorUtility.SaveFilePanel ("Create New File", "", "save001", "csv");
				newGameController saver = gameObject.GetComponent<newGameController> ();
				if (saver == null) {
						Debug.Log ("Error making new game");
				} else {
						saver.setName (filePath);
						saver.makeNewGame ();
				}
		}
		public override void runEsc ()
		{
				getInput ();
		}		
}
