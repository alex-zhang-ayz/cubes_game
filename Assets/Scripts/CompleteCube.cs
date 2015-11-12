using UnityEngine;
using System.Collections;

public class CompleteCube : MonoBehaviour {

	public GameObject faceSelObj;
	public GameObject selectionControl;
	public GameObject clkObj;
	public GameObject hsObj;
	public GameObject amObj;
	private AnimationManager am;
	private FaceSelection fs;
	private SelectionControl sc;
	private HighscoreTracker hsTracker;
	private ClickDetection cd;
	private GameObject cube;
	private bool canPlay = false;

	// Use this for initialization
	void Start () {
		am = amObj.GetComponent<AnimationManager> ();
		fs = faceSelObj.GetComponent<FaceSelection> ();
		hsTracker = hsObj.GetComponent<HighscoreTracker> ();
		sc = selectionControl.GetComponent<SelectionControl> ();
		cd = clkObj.GetComponent<ClickDetection> ();

	}

	public void setCube(GameObject g){
		cube = g;
	}

	public void playOnce(){
		canPlay = true;
	}

	// Update is called once per frame
	void Update () {
		if (cube != null) {
			CubeCreation ccg = cube.GetComponent<CubeCreation> ();
			am.setCube(this.cube.transform.GetChild(0).gameObject);
			am.setCC(ccg);
			if (sc.getHasValue ()) {
				if (ccg.getCenterNumber () == sc.getAnswer ().ToString ()) {
					hsTracker.addScore(int.Parse(ccg.getCenterNumber()), ccg.getDisabled(), ccg.scale);
					fs.addElement(ccg.getCenterNumber());
					am.playDestroy();
					cd.reset();
				}else{
					if (canPlay){
						canPlay = false;
						am.playTempRed();
					}
				}
			}
		}
	}
}
