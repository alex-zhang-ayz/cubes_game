using UnityEngine;
using System.Collections;

public class FaceBehaviour : MonoBehaviour {

	private Renderer faceRend; 	
	private float fixedTime;
	private float changeTime;
	private Color baseColor;
	private Color prevColor;
	private TextMesh tm;	//child's textmesh
	private bool disabled;
	private static float alpha = 0.8f;

	// Use this for initialization
	void Start () {
		faceRend = this.GetComponent<Renderer> ();
		Transform child = this.transform.GetChild (0);
		tm = child.GetComponent<TextMesh> ();
		fixedTime = Time.time;
		changeTime = -1;
		baseColor = faceRend.material.color;
		prevColor = baseColor;
		disabled = false;
	}

	public void setDisabled(bool b){
		disabled = b;
		if (disabled) {
			permColorChange (Color.grey);
		} else {
			permColorChange (baseColor);
		}
	}


	public void setNum(string s){
		tm.text = s;
	}

	public bool getDisabled(){
		return disabled;
	}

	public string getNum(){
		//The string should be easily converted for computation
		return tm.text;
	}

	public void permColorChange(Color c){
		c.a = alpha;
		faceRend.material.color = c;
		prevColor = c;
	}

	public void tempColorChange(Color c, float f){
		c.a = alpha;
		prevColor = faceRend.material.color;
		faceRend.material.color = c;
		changeTime = f;
		fixedTime = Time.time;
	}

	void Update () {
		if (changeTime >= 0 && Time.time - fixedTime > changeTime) {
			faceRend.material.color = prevColor;
			changeTime = -1;
		}
	}
}
