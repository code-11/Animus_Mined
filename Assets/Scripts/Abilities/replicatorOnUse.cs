using UnityEngine;
using System.Collections;

public class replicatorOnUse : placeOnUse {
		public override string provideBlock ()
		{
				return "prefabReplicator";
		}
}
