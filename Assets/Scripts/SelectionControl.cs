using UnityEngine;
using System.Collections;
using B83.ExpressionParser;

//Source: http://wiki.unity3d.com/index.php/ExpressionParser#ExpressionParser.cs

public class SelectionControl : MonoBehaviour {

	private bool infocus;
	private string expression;

	private bool[] canAdds; //0: number, 1: operation, 2: LBrack, 3: RBrack
	private int mode, prevMode;	//mode = 0 -> 1010, mode = 1 -> 0101
	private string lastAdded;
	[HideInInspector] public bool lastWasNum;
	private bool hasValue;
	private float vAnswer;

	void Start () {
		canAdds = new bool[4]{true, false, true, false};
		infocus = false;
		expression = "";
		lastAdded = "";
		hasValue = false;
		vAnswer = 0;
		mode = 0;
		prevMode = 0;
		lastWasNum = false;
	}

	void Update () {}

	public void calculate(){
		if (infocus) {
			try {
				var parser = new ExpressionParser ();
				Expression exp = parser.EvaluateExpression (expression);
				hasValue = true;
				vAnswer = (float)exp.Value;

			} catch (UnityException e) {
				print (e);
				hasValue = false;
				vAnswer = 0;
			}
		}
	}

	public string getExpr(){
		return expression;
	}

	public bool getHasValue(){
		return hasValue;
	}

	public float getAnswer(){
		return vAnswer;
	}

	public void backspace(){
		if (expression.Length >= lastAdded.Length) {
			expression = expression.Remove(expression.Length - lastAdded.Length, lastAdded.Length);
			setMode(prevMode);
		}
	}

	public void clear(){
		setMode (0);
		expression = "";
		lastAdded = "";
		hasValue = false;
		vAnswer = 0;
		prevMode = 0;
		lastWasNum = false;
	}


	public void addOperator(string s){
		if (infocus) {
			if ((s == "(" && canAdds[2]) || (s != ")" && s != "(" && canAdds[1])){
				lastAdded = s;
				expression += s;
				setMode(0);
				lastWasNum = false;
			}
			else if(s == ")" && canAdds[3]){
				lastAdded = s;
				expression += s;
				setMode(1);
				lastWasNum = false;
			}

		}
	}

	public void changefocus(bool b){
		infocus = b;
	}

	public void setMode(int i){	//mode = 0 -> 1010, mode = 1 -> 0101
		prevMode = mode;
		mode = i;
		switch (i) {
		case 0:
			canAdds[0] = true;
			canAdds[1] = false;
			canAdds[2] = true;
			canAdds[3] = false;
			break;
		case 1:
			canAdds[0] = false;
			canAdds[1] = true;
			canAdds[2] = false;
			canAdds[3] = true;
			break;
		}
	}


	//when out of focus
	public void reset(){
		infocus = false;
		setMode (0);
		expression = "";
		lastAdded = "";
		hasValue = false;
		vAnswer = 0;
		prevMode = 0;
		lastWasNum = false;
	}

	public bool getFocus(){
		return infocus;
	}

	public void addNum(string s){
		if (canAdds [0] && infocus) {
			lastAdded = s;
			expression += s;
			setMode(1);
			lastWasNum = true;
		}

	}

	public bool getCanAddNum(){
		return canAdds[0];
	}

}
