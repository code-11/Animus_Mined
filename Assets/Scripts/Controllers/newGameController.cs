using UnityEngine;
using System.Collections;

public class newGameController : MonoBehaviour
{
		private string m_fileName;
		public void setName (string name)
		{
				m_fileName = name;
		}

		public void makeNewGame ()
		{
				Debug.Log ("Make New Game Here");
		}
}
