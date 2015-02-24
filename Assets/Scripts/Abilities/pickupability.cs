using UnityEngine;
using System.Collections;

public class pickupability : MonoBehaviour
{

		void OnTriggerEnter (Collider other)
		{
				//Debug.Log ("Triggered");
				inventoryManager inven = other.gameObject.GetComponent<inventoryManager> ();
				if (inven != null) {
						inven.AddItem (gameObject);
						Destroy (this.gameObject);
				} else {
						Debug.Log ("Could not access Inventory");
				}
		}
}
