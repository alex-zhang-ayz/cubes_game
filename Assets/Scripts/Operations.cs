using UnityEngine;
using System.Collections;
using B83.ExpressionParser;

public class Operations : MonoBehaviour {

 	//Source: http://wiki.unity3d.com/index.php/ExpressionParser#ExpressionParser.cs

	void Start () {
		string s = "(4+1333)/1337+5-27*2";
		string f = "(3*(2))/6+6";
		var parser = new ExpressionParser();
		Expression exp = parser.EvaluateExpression(s);
		print (exp.Value);				
		exp = parser.EvaluateExpression(f);
		print (exp.Value);
	}

	void Update () {
	
	}
}
