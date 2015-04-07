using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public bool upMovePress(){
		return Input.GetKey("w");
	}
	public bool downMovePress(){
		return Input.GetKey("s");
	}
	public bool leftMovePress(){
		return Input.GetKey("a");
	}
	public bool rightMovePress(){
		return Input.GetKey("d");
	}
	public bool guiUpPress(){
		return Input.GetKeyDown("w");
	}
	public bool guiDownPress(){
		return Input.GetKeyDown("s");
	}
	public bool guiLeftPress(){
		return Input.GetKeyDown("a");
	}
	public bool guiRightPress(){
		return Input.GetKeyDown("d");
	}




	public bool upMinePress(){
		return Input.GetKey("up");
	}
	public bool downMinePress(){
		return Input.GetKey("down");
	}
	public bool leftMinePress(){
		return Input.GetKey("left");
	}
	public bool rightMinePress(){
		return Input.GetKey("right");
	}
	public bool invenLeftPress(){
		return Input.GetKeyDown("q");
	}
	public bool invenRightPress(){
		return Input.GetKeyDown("e");
	}
	public bool invenPress(){
		return Input.GetKeyDown("i");
	}
	public bool usePress(){
		return Input.GetKeyDown("r");
	}
	public bool useBuildPress(){
		return Input.GetKeyDown("f");
	}
	public bool dropPress(){
		return Input.GetKeyDown("p");
	}
	public bool alertPress(){
		return Input.GetKeyDown("c");
	}
	public bool escPress(){
		return Input.GetKeyDown("escape");
	}
}
