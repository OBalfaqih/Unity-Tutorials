using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public WheelCollider[] front_wheels;
    public float maxSpeed = 500.0f;
    public float maxSteer = 30.0f;

    private void FixedUpdate()
    {
        // Store the affected value of the torque speed by the user input
        float motorT = Input.GetAxis("Vertical") * maxSpeed;

        // Store the affected value of the steering by the user input
        float steerA = Input.GetAxis("Horizontal") * maxSteer;

        // Setting the torque speed for both wheels
        front_wheels[0].motorTorque = motorT;
        front_wheels[1].motorTorque = motorT;

        // Setting the steering angle for both wheels
        front_wheels[0].steerAngle = steerA;
        front_wheels[1].steerAngle = steerA;

        // If the user stopped controlling, will pull the brakes. Otherwise, release them
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f){
            front_wheels[0].brakeTorque = 0;
            front_wheels[1].brakeTorque = 0;
        }else{
            front_wheels[0].brakeTorque = 220;
            front_wheels[1].brakeTorque = 220;
        }
    }
}
