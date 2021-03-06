﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//using UnityEditor;
using System.IO;
using System.Text;
using System.Linq;

public class escManager : MonoBehaviour
{
		public class menuItem
		{
				private string m_desc;
				private Action m_cmd;
				
				public menuItem (string desc, Action cmd)
				{
						m_desc = desc;
						m_cmd = cmd;
				}
				public void useAction ()
				{
						m_cmd ();
				}
				public string getDesc ()
				{
						return m_desc;
				}
				
		}
		private alertManager alertCtrl;
		private InputManager inCtrl;
		public bool startingGame;
		public  int m_numSelected;
		public ArrayList m_menuList;
		public ArrayList m_startList;
		public ArrayList m_gameList; 
		public bool escMenuUp = false;
		public bool getEscMenuUp ()
		{
				return escMenuUp;
		}
		public virtual void addAllItems ()
		{

				m_menuList = new ArrayList ();	
				m_startList = new ArrayList();

				menuItem item1 = new menuItem ("Back to Main Menu", newFile);
				menuItem item2 = new menuItem ("Load Game", loadFile);
				menuItem item3 = new menuItem ("Save Game", saveFile);
				menuItem item4 = new menuItem ("Self-Destruct", exitGame);
				m_menuList.Add (item4);
				m_menuList.Add (item2);
				m_menuList.Add (item3);
				m_menuList.Add (item1);

				m_startList.Add(new menuItem ("New Game", newFile));
				m_startList.Add(new menuItem ("Load Game", loadFile));
				m_startList.Add(new menuItem ("Save Game", saveFile));
				m_startList.Add(new menuItem ("Exit Game", exitGame));

				m_gameList =m_menuList;

				m_numSelected = 0;


		}
		private void disableSpecial ()
		{
				GameObject special = GameObject.Find ("specialInvBlk");
				if (special != null) {
						special.SetActive (false);
				}
		}
		private void disableStartScreen()
		{
				GameObject title = GameObject.Find("OpenTitle");
				if (title!=null){
						title.SetActive(false);
				}
		}
		//Cleans up all the starting game objects and moves the player around.
		//Also takes away the gui.
		public void startGame ()
		{
				inCtrl=gameObject.GetComponent<InputManager>();
				QuickSlot quick = gameObject.GetComponent<QuickSlot> ();
				if (quick != null) {
						SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer> ();
						renderer.enabled = true;
						disableSpecial ();
						disableSpecial ();
						disableSpecial ();
						disableStartScreen();
						quick.enabled = true;
						if (escMenuUp) {
								escGui escMenu = gameObject.GetComponent<escGui> ();
								escMenu.enabled = false;
								escMenuUp = false;
						}
				}
		}
		public void Start ()
		{
			alertCtrl=gameObject.GetComponent<alertManager>();
			inCtrl=gameObject.GetComponent<InputManager>();
			addAllItems ();
		}

		void Update ()
		{
				runEsc ();
		}
		private menuItem getSelObj ()
		{
				if (m_menuList.Count > 0) {
						return (menuItem)m_menuList [m_numSelected];
				} else {
						return null;
				}
		}
		public int getSelNum ()
		{
				return m_numSelected;
		}
		public ArrayList getItems ()
		{
				return m_menuList;
		}		
		public void exitGame(){
			if(startingGame){
				Application.Quit();
			}else{
				deathController deathCtrl= gameObject.GetComponent<deathController>();
				deathCtrl.killSelf();
			}
		}
		public void loadFile ()
		{
				//string filePath = EditorUtility.OpenFilePanel ("Open File", "", "csv");
				string filePath="TheSave.csv";
				loadController loader = gameObject.GetComponent<loadController> ();
				bool fail=false;				
				try{
						StreamReader reader = new StreamReader (filePath, Encoding.Default);
				}catch{
						fail=true;
				}

				if ((loader == null) || (filePath == "") || (fail)) {
						alertCtrl.setAlert("Error Opening File");
						Debug.Log ("Error opening file");
				} else {
						if (startingGame) {
							startingGame = false;
							startGame ();
						}
						Debug.Log ("filePath" + filePath);
						loader.setName (filePath);
						//loader.switchLoad ();
						loader.LoadAll ();
				}
		}
		public void saveFile ()
		{
				//string filePath = EditorUtility.SaveFilePanel ("Open File", "", "save001", "csv");
				string filePath="TheSave.csv";
				saveController saver = gameObject.GetComponent<saveController> ();
				if ((saver == null) || (filePath == "")) { 
						alertCtrl.setAlert("Error Saving File");
						Debug.Log ("Error saving file");
				} else {
						saver.setName (filePath);
						saver.SaveAll ();
				}
		}
		public void newFile ()
		{
				if (startingGame) {
						startingGame = false;
						startGame ();
						newGameController constructer = gameObject.GetComponent<newGameController> ();
						constructer.makeNewGame ();
				}else{
						startingGame=true;
						Destroy (this.gameObject);
						Application.LoadLevel("startingScene");
				}
				
		
		}
		void shiftLeft ()
		{
				if (m_numSelected == 0) {
						m_numSelected = 0;
				} else {
						m_numSelected -= 1;
				}
		}
		void shiftRight ()
		{
				if (m_numSelected < m_menuList.Count - 1) {
						m_numSelected += 1;
				}
		}
		void UseSelected ()
		{
				getSelObj ().useAction ();
		}
		public virtual void runEsc ()
		{
				if (startingGame) {
						discreteMovement mov = gameObject.GetComponent<discreteMovement> ();
						m_menuList=m_startList;
/*						if (mov != null) {
								//mov.m_allowMovement = false;
						} else {
								Debug.Log ("Couldn't find discrete movement controller");
						}*/
				}else{
						m_menuList=m_gameList;
				}
				escGui escMenu = gameObject.GetComponent<escGui> ();
				bool escPress = inCtrl.escPress();

				if (escPress) {
						//Debug.Log ("pressed Esc");
						if (escMenuUp == true) {
								escMenuUp = false;
								escMenu.enabled = false;
						} else {
								escMenuUp = true;
								escMenu.enabled = true;
						}
						//StartCoroutine (viewIven ());
				}
				if (escMenuUp) {
						getInput ();
				}
		}
		public void getInput ()
		{
				bool selLeft = inCtrl.guiUpPress();
				bool selRight = inCtrl.guiDownPress();
				bool rPress = inCtrl.usePress();
				if (rPress) {
						UseSelected ();
				}
				if (selLeft) {
						shiftLeft ();
				}
				if (selRight) {
						shiftRight ();
				}
		}
}
