using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public float waitTime;
	private float timer;
	public Rigidbody fireBall;

	void Update(){
		if(timer > 0.0f)
			timer -= Time.deltaTime;
		if(timer <= 0.0f)
			spawnBall();
	}

	public void spawnBall(){
		Rigidbody tempBall = Instantiate(fireBall, transform.position, Quaternion.identity) as Rigidbody;
		tempBall.AddForce(new Vector3(Random.Range(-1, 1), Random.Range(-10, 10), Random.Range(-1, 1)) * Random.Range(100, 200));
		timer = Random.Range(waitTime, waitTime + 3.0f);
	}
}
