using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTest : MonoBehaviour {

	public Material blue;
	public Material red;

	public void changeBlue(){
		gameObject.GetComponent<Renderer>().material = blue;
	}

	public void changeRed(){
		gameObject.GetComponent<Renderer>().material = red;
	}
}
