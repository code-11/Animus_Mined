using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	bool m_horiz=false;
	bool m_vertUp=false;
	bool m_vertDown=false;

	public bool getAxisDown(bool theLock,string axis){
				if (Input.GetAxis (axis) != 0) {
					if (theLock == false) {
							return true;
							theLock = true;
						}
				} else {
						theLock = false;
				}
		return false;
	}
	public void Update(){
	}

	public bool upMovePress(){
		return (Input.GetKey("w")||Input.GetButton ("Up"));
	}
	public bool downMovePress(){
		return (Input.GetKey("s")||Input.GetButton ("Down"));
	}
	public bool leftMovePress(){
		return (Input.GetKey("a")||Input.GetButton ("Left"));
	}
	public bool rightMovePress(){
		return (Input.GetKey("d")||Input.GetButton ("Right"));
	}
	public bool guiUpPress(){
		return (Input.GetKeyDown("w")||Input.GetButtonDown ("Up"));
	}
	public bool guiDownPress(){
		return (Input.GetKeyDown("s")||Input.GetButtonDown ("Down"));
	}
	public bool guiLeftPress(){
		return (Input.GetKeyDown("a")||Input.GetButtonDown ("Left"));
	}
	public bool guiRightPress(){
		return (Input.GetKeyDown("d")||Input.GetButtonDown ("Right"));
	}




	public bool upMinePress(){
		return (Input.GetKey("up")||Input.GetAxis ("Vertical")>0);
	}
	public bool downMinePress(){
		return (Input.GetKey("down")||Input.GetAxis ("Vertical")<0);
	}
	public bool leftMinePress(){
		return (Input.GetKey("left")||Input.GetAxis ("Horizontal")<0);
	}
	public bool rightMinePress(){
		return (Input.GetKey("right")||Input.GetAxis ("Horizontal")>0);
	}
	public bool invenLeftPress(){
		return Input.GetKeyDown("q")||Input.GetButtonDown ("LeftInven");
	}
	public bool invenRightPress(){
		return Input.GetKeyDown("e")||Input.GetButtonDown ("RightInven");
	}
	public bool invenPress(){
		return Input.GetKeyDown("i");
	}
	public bool usePress(){
		return (Input.GetKeyDown("r")||Input.GetButtonDown ("Use"));
	}
	public bool useBuildPress(){
		return (Input.GetKeyDown("f")||Input.GetButtonDown ("Use2"));
	}
	public bool dropPress(){
		return Input.GetKeyDown("p");
	}
	public bool alertPress(){
		return Input.GetKeyDown("c");
	}
	public bool escPress(){
		return Input.GetKeyDown("escape")||Input.GetButtonDown("Esc");
	}
}
