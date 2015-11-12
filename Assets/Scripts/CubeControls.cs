using UnityEngine;
using System.Collections;

public class CubeControls : MonoBehaviour {

	public Vector3 rotateSpeed;
	private bool canRotate;
	private static float rotationMult = 2;

	void Start () {
		canRotate = true;
	}

	void Update () {
		//Rotation when not focused
		if (canRotate) {
			this.gameObject.transform.Rotate (rotateSpeed);
		}
		//Rotate with WASD if camera focused on it
		else {
			float horizontalValue = Input.GetAxis ("Horizontal");
			float verticalValue = Input.GetAxis ("Vertical");
			if (horizontalValue != 0 || verticalValue != 0) {
				this.gameObject.transform.Rotate (new Vector3 (verticalValue * rotationMult, 
				                                               0.0f, 
				                                               horizontalValue * rotationMult));
			}
		}
	}
	public void changeRotate(bool b){
		canRotate = b;
	}

}
