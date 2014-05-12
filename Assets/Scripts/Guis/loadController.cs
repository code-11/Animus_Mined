using UnityEngine;
using System.Collections;

public class loadController : MonoBehaviour
{

		// Use this for initialization
		int getCharges (GameObject go)
		{
				InvenObject item = go.GetComponent<InvenObject> ();
				if (item == null) {
						Debug.Log ("Item in inventory is not an Inven object!");
						return -1;
				} else {
						return item.getCharges ();
				}
		}
}
