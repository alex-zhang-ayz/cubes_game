using UnityEngine;
using System.Collections;

public class FaceTransplant : MonoBehaviour {

	private string savedButtonValue;
	private bool canChange;

	public string getSavedValue(){
		return savedButtonValue;
	}

	public void setSavedValue(string s){
		if (canChange) {
			savedButtonValue = s;
			print ("Here's the saved value: " +s);
		}
	}

	public bool getCanChange(){
		return canChange;
	}

	public void setCanChange(bool b){
		canChange = b;
	}

	public void reset(){
		canChange = false;
		savedButtonValue = "";
	}

	public void clearSaved(){
		savedButtonValue = "";
	}

	void Start () {
		canChange = false;
		savedButtonValue = "";
	}

	void Update () {
	
	}
}
