using UnityEngine;
using System.Collections;

public abstract class Usability : MonoBehaviour
{		
		public virtual bool Use (Transform playerPos)
		{
				return true;
		} 
}
