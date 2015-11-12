using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeCreation : MonoBehaviour {

	public float scale; // 0.3
	public Vector3 location; 
	public Vector3 centerAngle; // 0,45,45
	public GameObject textMesh; //base text prefab
	public Vector3 rotateSpeed; //0.5, 0.5, 0 <-could be randomized
	public float nullFaceChance = 0.6f;
	public Color baseColor = Color.black;
	public Color baseTextColor = Color.white;
	public Color baseCenterColor = Color.red;
	public bool isBoss = false;
	private GameObject blankParent;
	private GameObject[] planes;
	private GameObject[] textMeshes;
	private GameObject centerNumber;
	private int iCenterNumber = 0;
	private static float baseSize = 10;
	private static float baseAlpha = 0.8f;
	private Dictionary<int, GameObject> faceNums;
	private static float fractionOfSideSize = 6;

	
	void Start () {
		if (isBoss) {
			this.scale = this.scale * 10 + 10;
			this.baseTextColor = Color.red;
			this.baseCenterColor = Color.blue;
			this.nullFaceChance = 1f;
		}
		//The empty parent object
		blankParent = new GameObject ();
		blankParent.name = "numCube";
		CubeControls cc = blankParent.AddComponent<CubeControls> ();
		cc.rotateSpeed = this.rotateSpeed;
		blankParent.transform.SetParent (this.transform);
		/*
		BoxCollider bcp = blankParent.AddComponent<BoxCollider> ();
		bcp.center = Vector3.zero;
		bcp.size = new Vector3 (scale * baseSize, scale * baseSize, scale * baseSize);
		*/

		//CenterNumber
		centerNumber = GameObject.Instantiate (textMesh);
		centerNumber.transform.SetParent (blankParent.transform);
		centerNumber.transform.Rotate (centerAngle);

		TextMesh centerTM = centerNumber.GetComponent<TextMesh>();
		centerTM.text = iCenterNumber.ToString();
		
		Renderer cR = centerNumber.GetComponent<Renderer> ();
		cR.material.color = baseCenterColor;

		//Planes & child numbers
		GameObject newPlane, newTextMesh;
		planes = new GameObject[6];
		textMeshes = new GameObject[6];
		faceNums = new Dictionary<int, GameObject> ();

		float nullfacep = this.nullFaceChance;
		float nScale = (scale * baseSize) / 2;
		for (int i=0; i<6; i++) {
			newPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
			newPlane.name = "Face"+i;
			newPlane.transform.localScale = new Vector3 (scale, 1, scale);
			newPlane.transform.SetParent(blankParent.transform);
			newPlane.tag = "Face";
			Renderer r = newPlane.GetComponent<Renderer>();
			Color c = baseColor;
			c.a = baseAlpha;
			r.material.color = c;

			newTextMesh = GameObject.Instantiate(textMesh);
			newTextMesh.transform.SetParent(newPlane.transform);
			newTextMesh.transform.Rotate(new Vector3(90, 0,0));

			FaceBehaviour fb = newPlane.AddComponent<FaceBehaviour>();
			fb.name = newPlane.name + "'s Face Behaviour";
			TextMesh ntmTM = newTextMesh.GetComponent<TextMesh>();
			ntmTM.color = baseTextColor;

			float n = Random.value;
			if (n > nullfacep){
				int randNum = Random.Range(0, iCenterNumber * 2 + 7);
				while (faceNums.ContainsKey(randNum)){
					randNum = Random.Range(0, iCenterNumber * 2 + 7);
				}
				ntmTM.text = randNum.ToString();
				faceNums.Add(randNum, newPlane);
			}else{
				ntmTM.text = "";
			}
			nullfacep *= (nullFaceChance + (float)iCenterNumber/100);



			switch(i){
			case 0:
				newPlane.transform.position = new Vector3(newPlane.transform.position.x, 
				                                          newPlane.transform.position.y + nScale, 
				                                          newPlane.transform.position.z);

				break;
			case 1:
				newPlane.transform.Rotate(new Vector3(0,0,-180));
				newPlane.transform.position = new Vector3(newPlane.transform.position.x, 
				                                          newPlane.transform.position.y - nScale, 
				                                          newPlane.transform.position.z);
				break;
			case 2:
				newPlane.transform.Rotate(new Vector3(90,0,0));
				newPlane.transform.position = new Vector3(newPlane.transform.position.x, 
				                                          newPlane.transform.position.y, 
				                                          newPlane.transform.position.z + nScale);
				break;
			case 3:
				newPlane.transform.Rotate(new Vector3(-90,0,0));
				newPlane.transform.position = new Vector3(newPlane.transform.position.x, 
				                                          newPlane.transform.position.y, 
				                                          newPlane.transform.position.z - nScale);
				break;
			case 4:
				newPlane.transform.Rotate(new Vector3(0,0,-90));
				newPlane.transform.position = new Vector3(newPlane.transform.position.x + nScale, 
				                                          newPlane.transform.position.y, 
				                                          newPlane.transform.position.z);
				break;
			case 5:
				newPlane.transform.Rotate(new Vector3(0,0,90));
				newPlane.transform.position = new Vector3(newPlane.transform.position.x - nScale, 
				                                          newPlane.transform.position.y, 
				                                          newPlane.transform.position.z);
				break;
			}
			newTextMesh.transform.localScale = new Vector3(1,1,1);
			newTextMesh.GetComponent<TextMesh>().characterSize = nScale * 2.0f / fractionOfSideSize / scale;
			textMeshes[i] = newTextMesh;
			planes[i] = newPlane;

		}
		centerNumber.GetComponent<TextMesh> ().characterSize = nScale * 2.0f / fractionOfSideSize;
		blankParent.transform.position = location;
	}

	public void destroyCube(){
		destroyChildren (this.transform);
	}

	private void destroyChildren(Transform t){
		if (t.childCount > 0) {
			foreach (Transform child in t) {
				destroyChildren (child);
			}
		}
		GameObject.Destroy(t.gameObject);
	}

	public bool checkAllDisabled(){
		foreach (GameObject p in planes) {
			FaceBehaviour fb = p.GetComponent<FaceBehaviour>();
			if (!fb.getDisabled()){
				return false;
			}
		}
		return true;
	}

	public void resetColor(){
		foreach (GameObject p in planes) {
			FaceBehaviour fb = p.GetComponent<FaceBehaviour>();
			fb.setDisabled(false);
		}
	}

	public string getCenterNumber(){
		return centerNumber.GetComponent<TextMesh> ().text;
	}

	public void setCenterNumber(int i){
		iCenterNumber = i;

	}
	public int getDisabled(){
		int i = 0;
		foreach (GameObject g in planes) {
			FaceBehaviour fb = g.GetComponent<FaceBehaviour>();
			if (fb != null && fb.getDisabled()){
				i++;
			}
		}
		return i;
	}

	void Update () {

	}

}
