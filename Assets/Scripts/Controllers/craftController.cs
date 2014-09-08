using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class craftController : MonoBehaviour
{
		private class Recipe :Object
		{
				private Dictionary<string,int> m_input;
				private Dictionary<string,int> m_output;
				private int m_recipeNum;
				public Recipe (int num, Dictionary<string,int> react, Dictionary<string,int> prodct)
				{
						m_input = react;
						m_output = prodct;
						m_recipeNum = num;
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
		private ArrayList m_recipes = new ArrayList ();
		
		private string lookUpPrefab (string name)
		{
				switch (name) {
				case "Bomb":
						return "PickUps/prefabBombPickUp";
				case "Ladder":
						return "PickUps/prefabPickUp";
				default:
						return "PickUps/prefabSupportPickUp";
				}
		}
		
		private GameObject createResult (string name, int amount)
		{
				Debug.Log ("Trying to instantiate: " + name);
				string prefabName = lookUpPrefab (name);
				GameObject newObj = (GameObject)Instantiate ((GameObject)Resources.Load (prefabName), new Vector3 (0, 0, 0), Quaternion.identity);
				InvenObject chargeScript = newObj.GetComponent<InvenObject> ();
				chargeScript.setStackName (name);	
				chargeScript.setCharges (amount);
				newObj.SetActive (false);
				return newObj;
		}
		private Recipe getRecipeByNum (int num)
		{
				foreach (Recipe recipe in m_recipes) {
						if (recipe.getRecipeNum () == num) {
								return recipe;
						}
				}
				Debug.Log ("Recipe number not found");
				return null;
		}
		void Start ()
		{
		
				m_recipes.Add (new Recipe (0,
					new Dictionary<string,int>{{"Regolith",1},{"Nickel",1}},
					new Dictionary<string,int>{{"Bomb",2}}
				));
				m_recipes.Add (new Recipe (1,
		            new Dictionary<string,int>{{"Iron",1},{"Regolith",1}},
					new Dictionary<string,int>{{"Ladder",2}}
				));
		}
		void createRecipeByNum (int recipeNum)
		{
				ArrayList allowedNums = evalPossible ();
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
						Debug.Log ("Not allowed to make this recipe");
				}
		}
		ArrayList evalPossible ()
		{
				ArrayList possible = new ArrayList (); 
				inventoryManager inven = gameObject.GetComponent<inventoryManager> ();
				if (inven != null) {
						foreach (Recipe recipe in m_recipes) {
								int reqNum = recipe.getInput ().Count;
								int curNum = 0;
								foreach (var ingred in recipe.getInput()) {
										foreach (GameObject invenObj in inven.getInven()) {
												InvenObject objChargeScript = invenObj.GetComponent<InvenObject> ();
												if ((objChargeScript.m_stackName == ingred.Key) && (objChargeScript.m_charges >= ingred.Value)) {
														//Debug.Log (objChargeScript.m_stackName + " = " + ingred.Key);
														curNum += 1;
														
												} else {
														//Debug.Log (objChargeScript.m_stackName + " != " + ingred.Key);
												}
										}
										Debug.Log ("reqNum: " + reqNum + " curNum: " + curNum);
								}
								if (reqNum == curNum) {
										possible.Add (recipe.getRecipeNum ());
										Debug.Log ("adding to possible: " + recipe.getRecipeNum ());
								} else {
										Debug.Log ("Didn't make because reqNum: " + reqNum + " curNum: " + curNum);
								}
						}
				} else {
						Debug.Log ("Inventory componenet missing");
				}
				return possible;
				//Debug.Log (possible [0]);
		}
		// Update is called once per frame
		void Update ()
		{
				bool yPres = Input.GetKeyDown ("y");
				if (yPres) {
						createRecipeByNum (0);
				}
		}
}
