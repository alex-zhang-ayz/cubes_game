using UnityEngine;
using System.Collections;

public class HideScript : MonoBehaviour {

	public GameObject faceUI;

	void Start(){
		changeHide (faceUI);
	}


	public void changeHide(GameObject g){
		if (g.activeSelf) {
			g.SetActive (false);
		} else {
			g.SetActive (true);
		}
	}


}
