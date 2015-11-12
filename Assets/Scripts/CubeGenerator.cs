using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeGenerator : MonoBehaviour {

	public GameObject cubePrefab;
	public int firstNumbers = 15;
	public int maxValue = 100;
	public int createNum = 50;
	public float bossChance = 0.05f;
	private Dictionary<int, GameObject> cubeLocations;
	private static float minDistance = 5;
	private static float yValue = 2;


	// Use this for initialization
	void Start () {
		cubeLocations = new Dictionary<int, GameObject> ();
		firstXNumbers ();

		while (cubeLocations.Keys.Count < createNum) {
			int randNum = Random.Range(firstNumbers, maxValue);
			if (!cubeLocations.ContainsKey(randNum)){
				GameObject cube = GameObject.Instantiate(cubePrefab) as GameObject;
				CubeCreation cc_cube = cube.GetComponent<CubeCreation>();
				if (randNum > 1000 && Random.value < bossChance){
					cc_cube.isBoss = true;
				}
				Vector3 cubePos = findNewPos(randNum);
				cc_cube.location = cubePos;
				cc_cube.rotateSpeed = new Vector3(Random.value * 0.5f, Random.value * 0.5f, Random.value * 0.5f);
				cc_cube.setCenterNumber(randNum);
				cubeLocations.Add(randNum, cube);
			}
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void firstXNumbers(){
		for (int i=1; i<firstNumbers; i++) {
			if (!cubeLocations.ContainsKey(i)){
				GameObject cube = GameObject.Instantiate(cubePrefab) as GameObject;
				CubeCreation cc_cube = cube.GetComponent<CubeCreation>();
				Vector3 cubePos = findNewPos(i);
				cc_cube.location = cubePos;
				cc_cube.rotateSpeed = new Vector3(Random.value * 0.5f, Random.value * 0.5f, Random.value * 0.5f);
				cc_cube.setCenterNumber(i);
				cubeLocations.Add(i, cube);
			}

		}
	}

	private Vector3 findNewPos(int i){
		float floor = Mathf.Floor(i / 20.0f) * 20;
		/*
		float ceil = Mathf.Ceil(i / 20.0f) * 20;

		int negX = (int)Mathf.Round (Random.value);
		int negZ = (int)Mathf.Round (Random.value);
		*/
		float x, z;
		Vector3 pos = new Vector3 (0, yValue, 0);
		/*
		Collider[] neighbours;
		do {
			x = Random.Range (floor, ceil);
			z = Random.Range (floor, ceil);
			if (negX == 0) {
				x = -x;
			}
			if (negZ == 0) {
				z = -z;
			}
			pos = new Vector3 (x, yValue, z);
			neighbours = Physics.OverlapSphere (pos, minDistance);

		} while(neighbours.Length > 0);
		*/

		Collider[] neighbours;
		do{
			x = Random.Range(-20f, 20f);
			z = Random.Range(-20f, 20f);
			if (x < 0){
				x -= floor + Random.value * 20;
			}else{
				x += floor + Random.value * 20;
			}
			if (z < 0){
				z -= floor + Random.value * 20;
			}else{
				z += floor + Random.value * 20;
			}
			pos = new Vector3 (x, yValue, z);
			neighbours = Physics.OverlapSphere (pos, minDistance);
		}while(neighbours.Length > 0);

		return pos;
	}
}
