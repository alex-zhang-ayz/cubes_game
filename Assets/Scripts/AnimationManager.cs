using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {

	private GameObject cube;
	private CubeCreation cc;
	private float shrinkSpeed = 0.5f;
	private float targetScale = 0.01f;
	private bool playingDestroy = false;
	private static float basePlayTime = 1f;

	public void setCube(GameObject g){
		if (!playingDestroy) {
			cube = g;
		}
	}

	public void setCC(CubeCreation c){
		if (!playingDestroy) {
			cc = c;
		}
	}

	public void playTempRed(){
		Transform numCube = cube.transform.GetChild (0);
		foreach (Transform child in numCube) {
			if (child.gameObject.tag == "Face"){
				FaceBehaviour fb = child.GetComponent<FaceBehaviour>();
				fb.tempColorChange(Color.red, basePlayTime);
			}
		}

	}

	public void playDestroy(){
		if (cube != null && cc != null) {
			playingDestroy = true;
		}
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (playingDestroy) {
			cube.transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
			if (cube.transform.localScale.magnitude < (Vector3.one * targetScale).magnitude){
				cc.destroyCube();
				playingDestroy = false;
			}
		}
	}
}
