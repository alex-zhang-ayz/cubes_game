using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {
	private bool freeMove;
	// Use this for initialization
	void Start () {
		freeMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (freeMove) {
			float horizontalValue = Input.GetAxis ("Horizontal");
			float verticalValue = Input.GetAxis ("Vertical");
			float rotateValue = Input.GetAxis ("Rotate");
			if (horizontalValue != 0 || verticalValue != 0) {
				this.gameObject.transform.Translate (new Vector3 (horizontalValue, 0.0f, verticalValue));
			}
			if (rotateValue != 0){
				this.gameObject.transform.Rotate(new Vector3 (0, rotateValue, 0));
			}
		}
	}
	public void changeFreeMove(bool b){
		freeMove = b;
	}
}
