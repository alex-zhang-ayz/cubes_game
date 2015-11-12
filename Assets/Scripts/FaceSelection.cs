using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class FaceSelection : MonoBehaviour {

	public GameObject buttonPrefab;
	public GameObject faceTObj;
	private static int spacing = 30;
	private static int numStored = 50;
	private static int maxHas = 5;
	private ArrayList collectedNumbers;
	private string[] firstfew;
	private int index;

	private RectTransform rekt;
	private float height;
	private GameObject[] buttons;
	private int numbuttons = 0;
	private float relativeHeight;
	
	void Start () {
		buttons = new GameObject[numStored];
		index = 0;
		firstfew = new string[numStored];
		collectedNumbers = new ArrayList ();

		rekt = this.GetComponent<RectTransform> ();
		this.height = rekt.rect.height;
		relativeHeight = height / 2 - spacing;

		for (int i=0; i<numStored; i++){
			if (relativeHeight < ((-1)*height/2 + spacing)){
				numbuttons = i;
				break;
			}
			GameObject newButton = GameObject.Instantiate(buttonPrefab) as GameObject;
			newButton.tag = "UI";
			newButton.transform.SetParent(this.transform);
			newButton.transform.localScale = Vector3.one;
			newButton.transform.localPosition = new Vector3(0, relativeHeight,0);
			ButtonBehaviour bb = newButton.GetComponent<ButtonBehaviour>();
			if (bb != null){
				bb.ftObj = faceTObj;	
			}
			buttons[i] = newButton;
			relativeHeight -= spacing + newButton.GetComponent<RectTransform>().rect.height/2;
		}
		for (int i=0; i<= maxHas; i++) {
			this.addElement(i.ToString());
		}

	}

	public void addElement(string s){

		if (Array.IndexOf (firstfew, s) == -1 && !collectedNumbers.Contains (s)) {
		
			if (index == numStored) {
				collectedNumbers.Add (s);
				collectedNumbers.Sort ();
			} else {
				firstfew [index] = s;
				index++;
			}
			int minimum = Mathf.Min (index, numbuttons);
			for (int i=0; i<minimum; i++) {
				Text t = buttons [i].transform.GetChild (0).GetComponent<Text> ();
				if (t != null) {
					t.text = firstfew [i];
				}
			}
		}
	}


	void Update () {

	}
}
