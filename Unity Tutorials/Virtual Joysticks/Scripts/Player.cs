using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	public Rigidbody rb;
	public float speed = 90.0f;
	private float maxSpeed = 9.0f;
	public int points = 0;
	public Text text;
	public GameObject coinSound;
	public AudioClip c;

	void FixedUpdate(){
		if(rb.velocity.magnitude < maxSpeed)
			rb.AddForce(new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal") * speed, 0, CrossPlatformInputManager.GetAxisRaw("Vertical") * speed));
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Coin"){
			AudioSource.PlayClipAtPoint(c, col.transform.position);
			Destroy(col.gameObject);
			points++; //points = points + 1; points += 1;
			text.text = points.ToString();
//			Instantiate(coinSound, col.transform.position, Quaternion.identity);
		}

		if(col.tag == "Out"){
			transform.position = new Vector3(0.0f, 1f, 0.0f);
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.collider.tag == "Fire"){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
