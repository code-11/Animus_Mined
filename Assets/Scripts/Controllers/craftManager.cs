﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class craftManager : MonoBehaviour
{
		public class Recipe :Object
		{
				private Dictionary<string,int> m_input;
				private Dictionary<string,int> m_output;
				private int m_recipeNum;
				private bool m_locked;
				public Recipe (int num, Dictionary<string,int> react, Dictionary<string,int> prodct, bool locked)
				{
						m_input = react;
						m_output = prodct;
						m_recipeNum = num;
						m_locked = locked;
				}  
				public bool isLocked ()
				{
						return m_locked;
				}
				public void setLocked (bool locked)
				{
						m_locked = locked;
				}
				public Dictionary<string,int> getInput ()
				{
						return m_input;
				}
				public Dictionary<string,int> getOutput ()
				{
						return m_output;
				}
				public int getRecipeNum ()
				{
						return m_recipeNum;
				}
		}
		private alertManager alertCtrl;
		private InputManager inCtrl;
		private ArrayList m_unlocked = new ArrayList ();
		private ArrayList m_allRecipes = new ArrayList ();
		private bool guiMenuUp = false;
		private craftGui m_craftGui;
		private int numSelected = 0;
		
		public int getNumSelected ()
		{
				return numSelected;
		}
		public bool getGuiMenuUp ()
		{
				return guiMenuUp;
		}
		public ArrayList getAllRecipesIncludingLock ()
		{
				return m_allRecipes;
		}
		public ArrayList getRecipes ()
		{
				return m_unlocked;
		}
		public void activate (int recipeNum)
		{
				((Recipe)m_allRecipes [recipeNum]).setLocked (false);
				calculateUnlocked ();
		}
		
		private string lookUpPrefab (string name)
		{
				switch (name) {
				case "Bomb":
						return "PickUps/prefabBombPickUp";
				case "Ladder":
						return "PickUps/prefabPickUp";
				case "Support":
						return "PickUps/prefabSuppPickUp";
				case "Factory":
						return "PickUps/prefabFactPickUp";
				case "Replicator":
						return "PickUps/prefabReprPickUp";
				default:
						return "PickUps/prefabPickUp";
				}
		}
		
		private GameObject createResult (string name, int amount)
		{
				//Debug.Log ("Trying to instantiate: " + name);
				string prefabName = lookUpPrefab (name);
				GameObject newObj = (GameObject)Instantiate ((GameObject)Resources.Load (prefabName), new Vector3 (0, 0, 0), Quaternion.identity);
				newObj.name= newObj.name.Remove(newObj.name.Length-7,7);

				InvenObject chargeScript = newObj.GetComponent<InvenObject> ();
				chargeScript.setStackName (name);	
				chargeScript.setCharges (amount);
				newObj.SetActive (false);
				return newObj;
		}
		private Recipe getRecipeByNum (int num)
		{
				foreach (Recipe recipe in m_unlocked) {
						if (recipe.getRecipeNum () == num) {
								return recipe;
						}
				}
				Debug.Log ("Recipe number not found");
				return null;
		}
		public void calculateUnlocked ()
		{
				m_unlocked = new ArrayList ();
				foreach (Recipe theRecipe in m_allRecipes) {
						if (!theRecipe.isLocked ()) {
								m_unlocked.Add (theRecipe);
						}
				}
		}
		void Start ()
		{
				m_craftGui = gameObject.GetComponent<craftGui> ();
				alertCtrl=gameObject.GetComponent<alertManager>();
				inCtrl=gameObject.GetComponent<InputManager>();

				
				m_allRecipes.Add (new Recipe (0,
		            new Dictionary<string,int>{{"Regolith",4}},
					new Dictionary<string,int>{{"Rock",1}},
					false
				));
				m_allRecipes.Add (new Recipe (1,
		            new Dictionary<string,int>{{"Regolith",2}},
					new Dictionary<string,int>{{"Ladder",1}},
					false
				));

				m_allRecipes.Add (new Recipe (2,
		            new Dictionary<string,int>{{"Regolith",1},{"Rock",1}},
					new Dictionary<string,int>{{"Support",1}},
					true
				));
				m_allRecipes.Add (new Recipe (3,
					new Dictionary<string,int>{{"Iron",1},{"Nickel",1},{"Magnesium",1}},
					new Dictionary<string,int>{{"Bomb",1}},
					false
				));
				m_allRecipes.Add (new Recipe (4,
		            new Dictionary<string,int>{{"Iron",2},{"Quartz",1},{"Nickel",1},{"Rock",2}},
					new Dictionary<string,int>{{"Factory",1}},
					false
				));
				m_allRecipes.Add (new Recipe (5,
		            new Dictionary<string,int>{{"Iron",4},{"Quartz",2},{"Rock",4}},
					new Dictionary<string,int>{{"Replicator",1}},
					false
				));
				calculateUnlocked ();
		}
		void createRecipeByNum (int recipeNum)
		{
				ArrayList allowedNums = evalPossible (recipeNum);
				if (allowedNums.Contains (recipeNum)) {
						Recipe desRecipe = getRecipeByNum (recipeNum);
						inventoryManager inven = gameObject.GetComponent<inventoryManager> ();
						//Removes all the ingredients from your inventory
						foreach (var ingred in desRecipe.getInput()) {
								ArrayList invenList = inven.getInven ();
								for (int i=0; i<invenList.Count; i+=1) {
										GameObject invenObj = (GameObject)(invenList [i]);
										InvenObject objChargeScript = invenObj.GetComponent<InvenObject> ();
										if (objChargeScript.m_stackName == ingred.Key) {
												objChargeScript.incrCharges (-ingred.Value);
												if (objChargeScript.getCharges () == 0) {
														invenList.RemoveAt (i);
														break;
												} else if (objChargeScript.getCharges () < 0) {
														Debug.Log ("Error in craft controller, charges negative");
												}
										}
								}
						}
						//add all results of the recipe into your inventory
						foreach (var result in desRecipe.getOutput()) {
								GameObject resultObj = createResult (result.Key, result.Value);
								inven.AddItem (resultObj);
						}
						
				} else {
						//alertCtrl.setAlert("Not allowed to make this recipe");
						Debug.Log ("Not allowed to make this recipe");
				}
		}
		ArrayList evalPossible (int recipeNum)
		{
				ArrayList possible = new ArrayList (); 
				inventoryManager inven = gameObject.GetComponent<inventoryManager> ();
				if (inven != null) {
						foreach (Recipe recipe in m_unlocked) {
								//reqNum is confusingly, number of requirements, ie if 1A +2B =3C, reqNum=2
								int reqNum = recipe.getInput ().Count;
								int curNum = 0;
								string debugInfo="";
								foreach (var ingred in recipe.getInput()) {
										foreach (GameObject invenObj in inven.getInven()) {
												InvenObject objChargeScript = invenObj.GetComponent<InvenObject> ();
												if ((objChargeScript.m_stackName == ingred.Key) && (objChargeScript.m_charges >= ingred.Value)) {
														//Debug.Log (objChargeScript.m_stackName + " = " + ingred.Key);
														curNum += 1;
														
												} 
												if((objChargeScript.m_stackName==ingred.Key)&&(objChargeScript.m_charges<ingred.Value)) {
													if (recipe.getRecipeNum()==recipeNum){
														debugInfo="Didn't make because needed " + ingred.Value +" "+ingred.Key+" but inventory has "+objChargeScript.m_charges;
													}
												}
										}
										//Debug.Log ("reqNum: " + reqNum + " curNum: " + curNum);
								}
								if (reqNum == curNum) {
										possible.Add (recipe.getRecipeNum ());
										Debug.Log ("adding to possible: " + recipe.getRecipeNum ());
								} else {
									if (recipe.getRecipeNum()==recipeNum){
										Debug.Log ("Couldn't make recipe");
										if (debugInfo!=""){
											//If had ingredient but not enough charges
											alertCtrl.setAlert(debugInfo);
										}else{
											alertCtrl.setAlert("Didn't make because missing ingredient");
										}
									}
								}
						}
				} else {
						Debug.Log ("Inventory component missing");
				}
				return possible;
				//Debug.Log (possible [0]);
		}
		public bool checkForFactory ()
		{
				int onlyPlayer = 1 << 9;
				int allButPlayer = ~onlyPlayer;
				Collider2D hit = Physics2D.OverlapPoint (new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y), allButPlayer);
				if (hit != null) {
						return hit.name == "prefabFactory" || hit.name == "prefabFactory(Clone)";
				} else {
						return false;
				}
		}
		// Update is called once per frame
		public void selection ()
		{
				bool wPres = inCtrl.guiUpPress();
				bool aPres = inCtrl.guiLeftPress();
				bool sPres = inCtrl.guiDownPress();
				bool dPres = inCtrl.guiRightPress();
				bool rPres = inCtrl.usePress();
				int MAX_NUM_PER_ROW = 3;
				int MAX_NUM_PER_COLUMN = 4;
				int NUM_COLS = m_unlocked.Count / MAX_NUM_PER_COLUMN;
				int LAST_NUM_PER_ROW = m_unlocked.Count % MAX_NUM_PER_COLUMN;
				
				if (wPres) {
						if ((numSelected % MAX_NUM_PER_COLUMN) != 0) {
								numSelected -= 1;
								
						}
						
				} else if (sPres) {
						if ((numSelected + 1) % MAX_NUM_PER_COLUMN != 0) {
								if (numSelected + 1 < m_unlocked.Count) {
										numSelected += 1;
								}
								
						}
				} else if (aPres) {
						if (numSelected > MAX_NUM_PER_COLUMN - 1) {
								numSelected -= MAX_NUM_PER_COLUMN;
								
						}
				} else if (dPres) {
						if ((numSelected < (MAX_NUM_PER_COLUMN * (MAX_NUM_PER_ROW - 1))) && (numSelected + MAX_NUM_PER_COLUMN < m_unlocked.Count)) {
								numSelected += MAX_NUM_PER_COLUMN;	
						}
				}
				if (rPres) {
						createRecipeByNum (((Recipe)m_unlocked [numSelected]).getRecipeNum ());
				}
				
		}
		public void runCraft ()
		{
				bool yPres = inCtrl.useBuildPress();
				if (yPres && checkForFactory ()) {
						if (guiMenuUp == true) {
								guiMenuUp = false;
								m_craftGui.enabled = false;
						} else {
								guiMenuUp = true;
								m_craftGui.enabled = true;
						}
						//createRecipeByNum (0);
				}
				if (guiMenuUp) {
						selection ();
				}
		}
		void Update ()
		{
				runCraft ();
		}
}
