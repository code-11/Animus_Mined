using UnityEngine;
using System.Collections;

public class ladderController : MonoBehaviour
{
		public int m_ladderSpeed;
		public float ladderCheckScale;
		// Use this for initialization
		void ladderNavigate ()
		{
				float vertDir = Input.GetAxis ("Vertical");
				Vector3 amountMove = (new Vector3 (0, vertDir) * m_ladderSpeed * Time.fixedDeltaTime);
				Collider2D[] coll = new Collider2D[2];
				float scale = ladderCheckScale;
				int numHit = Physics2D.OverlapCircleNonAlloc ((Vector2)transform.position, scale, coll);
				bool hitLadder = false;
				if ((numHit > 1)) {
						hitLadder = ((coll [1].CompareTag ("Ladder"))) || (coll [0].CompareTag ("Ladder"));
						if (hitLadder)
								transform.position += amountMove;
				}
		}
		void FixedUpdate ()
		{
				ladderNavigate ();
		}
}
