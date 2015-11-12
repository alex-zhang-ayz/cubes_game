using UnityEngine;
using System.Collections;

public class CubeManipulation : MonoBehaviour {

	public GameObject clickdetectObj;
	private ClickDetection cd;
	private GameObject parentCube;
	private GameObject selectedFace;
	private GameObject prevCube;
	private GameObject prevFace;
	private FaceBehaviour fb;
	private string sc_answer;

	//Change Color: setDisabled, flash red, end sequence?
	//Set numbers
	
	void Start () {
		cd = clickdetectObj.GetComponent<ClickDetection> ();
		sc_answer = "";
	}

	void Update () {
		if (parentCube != null) {
			GameObject root = parentCube.transform.root.gameObject;
			CubeCreation ccr;
			if ((ccr = root.GetComponent<CubeCreation>()) != null){
				if (ccr.getCenterNumber() == sc_answer){
					ccr.destroyCube();
					cd.reset();
				}
			}
		}

	}

	public bool clickFace(bool canAddNum){
		fb = this.selectedFace.GetComponent<FaceBehaviour> ();
		print (fb.getNum());
		if (fb != null && !fb.getNum().Equals ("") && !fb.getDisabled () && canAddNum) {
			print ("im here");
			fb.setDisabled(true);
			return true;
		}
		return false;
	}

	public string getNum(){
		fb = this.selectedFace.GetComponent<FaceBehaviour> ();
		if (fb != null) {
			return fb.getNum();
		}
		return "";
	}

	public void setParentCube(GameObject g){
		if (parentCube != null) {
			prevCube = parentCube;
		}
		parentCube = g;
	}

	public void setSelectedFace(GameObject g){
		if (prevFace != null) {
			prevFace = selectedFace;
		}
		selectedFace = g;
	}
	public GameObject getPrevCube(){
		return this.prevCube;
	}
	public GameObject getParentCube(){
		return this.parentCube;
	}
	public GameObject getSelectedFace(){
		return this.selectedFace;
	}

}
