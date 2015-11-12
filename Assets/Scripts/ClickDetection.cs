using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ClickDetection : MonoBehaviour {
	public GameObject cubeSelection;
	//public GameObject cubeManipObject;
	public GameObject compCubeObject;
	public GameObject faceTransplantObject;
	private FaceTransplant ft;
	private SelectionControl sc;
	private CubeControls cc;
	private CompleteCube compc;
	private static float yPos = 5;
	private GameObject prevCube;
	private FaceBehaviour[] prevFaces;
	private int location;

	// Use this for initialization
	void Start () {
		cc = null;
		prevFaces = new FaceBehaviour[6];
		location = -1;
		ft = faceTransplantObject.GetComponent<FaceTransplant> ();
		compc = compCubeObject.GetComponent<CompleteCube> ();
		sc = cubeSelection.GetComponent<SelectionControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Pressed left click, casting ray.");
			CastRay();

		}
		//temp
		if (Input.GetMouseButtonDown (1)) {
			if (sc.getFocus()){
				sc.calculate();
			}	
		}
	}
	void CastRay() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		//if the ray hit something (collision)
		if (Physics.Raycast (ray, out hit, 100)) {
			GameObject colthing = hit.collider.gameObject;
			//if clicked a face
			if (colthing.tag == "Face") {
				//First cube or Different cube
				if (cc == null || (prevCube != null && prevCube != colthing.transform.parent.gameObject)){
					if (prevCube != null && prevCube != colthing.transform.parent.gameObject){
						reset ();
					}
					compc.setCube(colthing.transform.root.gameObject);
					cc = colthing.GetComponentInParent<CubeControls> ();
					cc.changeRotate (false);
					Camera.main.transform.LookAt (colthing.transform);
					Camera.main.GetComponent<CameraControls> ().changeFreeMove (false);
					sc.changefocus(true);
					ft.setCanChange(true);
					prevCube = colthing.transform.parent.gameObject;

				}
				//Clicked same cube
				else{
					if (sc.getFocus()){
						FaceBehaviour fb = colthing.GetComponent<FaceBehaviour>();

						if (fb != null && !fb.getNum().Equals("") &&!fb.getDisabled()){
							if (sc.getCanAddNum()){
								fb.setDisabled(true);
								if (location < 0){
									location = 0;
								}
								prevFaces[location] = fb;
								location++;

							}
							sc.addNum(fb.getNum());
						}else if (fb != null && fb.getNum().Equals("")){
							if (ft.getSavedValue() != ""){
								fb.setNum (ft.getSavedValue());
								ft.clearSaved();
							}
						}
					}
				}
			}
			Debug.DrawLine (ray.origin, hit.point);
			Debug.Log ("Hit object: " + hit.collider.gameObject.name);
		}
		//no collision, clicking air
		else if (EventSystem.current.currentSelectedGameObject == null || 
		         (EventSystem.current.currentSelectedGameObject != null 
		 && EventSystem.current.currentSelectedGameObject.tag != "UI")	){ //change to doubleclick!!
			reset();
		}

	}
	/*
	public void backspace(){
		if (sc.lastWasNum) {
			foreach (FaceBehaviour fb in prevFaces) {
				print (fb.name);
			}
			print (location);
			if (location >= 0) {

				prevFaces [location].setDisabled (false);
			}
		}
	}
*/
	public void colorReset(){
		if (prevCube != null){
			GameObject creator = prevCube.transform.parent.gameObject;
			if (creator != null){
				creator.GetComponent<CubeCreation>().resetColor();
			}
		}

	}
	public void reset(){
		if (cc != null)
			cc.changeRotate(true);
		compc.setCube (null);

		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,
		                                             yPos,
		                                             Camera.main.transform.position.z);
		Camera.main.transform.rotation = Quaternion.identity;

		Camera.main.GetComponent<CameraControls> ().changeFreeMove (true);
		cc = null;
		if (prevCube != null){
			GameObject creator = prevCube.transform.parent.gameObject;
			if (creator != null){
				creator.GetComponent<CubeCreation>().resetColor();
			}
		}
		location = -1;
		ft.reset ();
		sc.reset();

	}
}
