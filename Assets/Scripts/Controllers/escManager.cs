﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;

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
		// Use this for initialization
		private int m_numSelected;
		private ArrayList m_menuList;
		private bool escMenuUp = false;
		
		void Start ()
		{
				m_menuList = new ArrayList ();	
				menuItem item1 = new menuItem ("Load Game", loadFile);
				menuItem item2 = new menuItem ("Save Game", saveFile);
				m_menuList.Add (item1);
				m_menuList.Add (item2);
				m_numSelected = 0;
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
		
		void loadFile ()
		{
				string filePath = EditorUtility.OpenFilePanel ("Open File", "", "csv");
		}
		void saveFile ()
		{
				Debug.Log ("Saving");
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
		void runEsc ()
		{
				escGui escMenu = gameObject.GetComponent<escGui> ();
				bool selLeft = Input.GetKeyDown ("q");
				bool selRight = Input.GetKeyDown ("e");
				bool rPress = Input.GetKeyDown ("r");
				bool escPress = Input.GetKeyDown ("escape");
				if (rPress) {
						UseSelected ();
				}
				if (selLeft) {
						shiftLeft ();
				}
				if (selRight) {
						shiftRight ();
				}
				if (escPress) {
						Debug.Log ("pressed Esc");
						if (escMenuUp == true) {
								escMenuUp = false;
								escMenu.enabled = false;
						} else {
								escMenuUp = true;
								escMenu.enabled = true;
						}
						//StartCoroutine (viewIven ());
				}
		}
}
