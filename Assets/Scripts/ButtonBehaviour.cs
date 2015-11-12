using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonBehaviour : MonoBehaviour {

	public GameObject ftObj;
	private FaceTransplant ft;
	private Button button;
	private Text text;

	// Use this for initialization
	void Start () {
		text = this.transform.GetChild (0).GetComponent<Text> ();
		ft = ftObj.GetComponent<FaceTransplant> ();
		button = this.GetComponent<Button> ();
		button.onClick.AddListener (delegate {saveFace();});
	}

	private void saveFace(){
		if (ft != null) {
			ft.setSavedValue(text.text);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
