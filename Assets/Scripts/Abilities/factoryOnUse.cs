using UnityEngine;
using System.Collections;

public class factoryOnUse : placeOnUse {
		public override string provideBlock ()
		{
				return "prefabFactory";
		}
}
