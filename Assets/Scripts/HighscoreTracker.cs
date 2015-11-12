using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighscoreTracker : MonoBehaviour {

	public GameObject textField;
	private Text tfT;
	private float score;

	public void addScore(int centerNumber, int numDisabled, float scale){
		float tobeadded = centerNumber * (100 * numDisabled) + 1000 * scale;
		score += tobeadded;
	}

	// Use this for initialization
	void Start () {
		score = 0;
		tfT = textField.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (tfT != null) {
			tfT.text = score.ToString();
		}
	}


}
