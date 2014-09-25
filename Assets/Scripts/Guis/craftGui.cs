using UnityEngine;
using System.Collections;

public class craftGui : MonoBehaviour
{
		private craftManager m_craftCtrl;
		private float width = 800;
		private float height = 600;
		private float centerX;
		private float localY;
		private float leftMost;
		private float bottom;
		// Use this for initialization
		void OnGUI ()
		{
				runGui ();
		}
		private void Start ()
		{
				m_craftCtrl = gameObject.GetComponent<craftManager> ();
				
		}
		private void makeBackround ()
		{
				centerX = Screen.width / 2;
				float centerY = Screen.height / 2;
				float localX = centerX - (width / 2);
				localY = centerY - (height / 2);
				leftMost = localX;
				bottom = centerY + (height / 2);
				GUI.Box (new Rect (localX, localY, width, height), "Crafting");
		}
		private void drawAllRecipes ()
		{
				
				float tempX = leftMost + 20;
				float tempY = localY + 50;
				int yPad = 70;
				int xPad = 220;
				int recipeNum = 0;
				foreach (craftManager.Recipe recipe in m_craftCtrl.getRecipes()) {
						if (recipeNum == m_craftCtrl.getNumSelected ()) {
								drawRecipe (recipe, tempX, tempY, true);
						} else {
								drawRecipe (recipe, tempX, tempY);
						}
						tempY += yPad;
						if (tempY + yPad > bottom) {
								tempY = localY + 50;
								tempX += xPad;
						}
						recipeNum += 1;			
						
				}
		}
		private void drawRecipe (craftManager.Recipe recipe, float x, float y, bool makeSelected=false)
		{
				string renderText = "";
				int i = 0;
				foreach (var input in recipe.getInput()) {
						renderText += (input.Value + " " + input.Key);
						if (i != recipe.getInput ().Count - 1) {
								renderText += " + ";
						}
						i += 1;
				}
				renderText += " = ";
				i = 0;
				foreach (var output in recipe.getOutput()) {
						renderText += (output.Value + " " + output.Key);
						if (i != recipe.getOutput ().Count - 1) {
								renderText += " + ";
						}
						
				}
				if (makeSelected) {
						GUI.color = Color.yellow;
				} else {
						GUI.color = Color.white;
				}
				GUI.Button (new Rect (x, y, 200, 50), renderText);
				//GUI.Box (new Rect (localX, localY, width, height), "Menu");
		}
		void runGui ()
		{
				makeBackround ();
				drawAllRecipes ();
				//drawRecipe ((craftController.Recipe)m_craftCrl.getRecipes () [0], leftMost, localY);
		}
}
