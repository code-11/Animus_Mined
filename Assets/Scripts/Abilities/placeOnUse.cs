using UnityEngine;
using System.Collections;

public abstract class placeOnUse : Usability
{
		private GameObject m_block;
		
		public virtual string provideBlock ()
		{
				return "Error provideBlock not overriden";
		}
		
		void OnEnable ()
		{
				m_block = Resources.Load (provideBlock ()) as GameObject;
		}
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
		public override bool Use (Transform playerPos)
		{
				//Debug.Log ("Ladder was used");
		
				int x = (int)Mathf.Round (playerPos.position.x);
				int y = (int)Mathf.Round (playerPos.position.y);
				int onlyPlayer = 1 << 9;
				int allButPlayer = ~onlyPlayer;
				Collider2D blockerInfo = Physics2D.OverlapCircle (new Vector2 (x, y), .4f, allButPlayer);
				if (blockerInfo == null) {
						GameObject newObj=(GameObject)Instantiate (m_block, new Vector3 (x, y, 0), Quaternion.identity);
						if (newObj!=null){
								newObj.name= newObj.name.Remove(newObj.name.Length-7,7);
						}else{
								Debug.Log("Error in placeonUse, prefab failed to instatiate");
						}
						return true;
				} else {
						return false;
				}
				//Debug.Log ("PlayerPosx: " + playerPos.position.x + " Roundedx: " + x);
		
		}
}
