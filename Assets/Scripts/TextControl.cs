using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextControl : MonoBehaviour {

	public GameObject selectionControlGameObect;
	private SelectionControl sc;
	private bool hasValue;
	private float answer;
	private GameObject expr, ans;
	private Text exprT, ansT;

	void Start () {
		sc = selectionControlGameObect.GetComponent<SelectionControl> ();
		expr = this.transform.Find ("Expression").gameObject;
		ans = this.transform.Find ("Answer").gameObject;
		print (expr);
		exprT = expr.GetComponent<Text> ();
		ansT = ans.GetComponent<Text> ();
	}

	void Update () {
		if (sc.getFocus ()) {
			hasValue = sc.getHasValue ();
			if (hasValue) {
				answer = sc.getAnswer ();
				ansT.text = answer.ToString ();
			}
			exprT.text = sc.getExpr ();
		} else {
			ansT.text = "";
			exprT.text = "";
		}
	}
}
