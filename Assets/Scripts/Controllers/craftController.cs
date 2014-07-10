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
						m_recipeNum = m_recipeNum;
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
	
		void Start ()
		{
		
				m_recipes.Add (new Recipe (0,
					new Dictionary<string,int>{{"Regolith",1}},
					new Dictionary<string,int>{{"Bomb",2}}
				));
		}
	
		void evalPossible ()
		{
				ArrayList possible = new ArrayList (); 
				inventoryManager inven = gameObject.GetComponent<inventoryManager> ();
				if (inven != null) {
						foreach (Recipe recipe in m_recipes) {
								foreach (var ingred in recipe.getInput()) {
										foreach (GameObject invenObj in inven.getInven()) {
												InvenObject objChargeScript = invenObj.GetComponent<InvenObject> ();
												if ((objChargeScript.m_stackName == ingred.Key) && (objChargeScript.m_charges >= ingred.Value)) {
														Debug.Log (objChargeScript.m_stackName + " = " + ingred.Key);
														possible.Add (recipe.getRecipeNum ());
												} else {
														Debug.Log (objChargeScript.m_stackName + " != " + ingred.Key);
												}
										}
								}
						}
				} else {
						Debug.Log ("Inventory componenet missing");
				}
				Debug.Log (possible [0]);
		}
		// Update is called once per frame
		void Update ()
		{
				bool yPres = Input.GetKeyDown ("y");
				if (yPres) {
						evalPossible ();
				}
		}
}
