using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.right, transform.rotation.y + 10.0f);
	}
}
