using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
* Created by: Omar Balfaqih (@OBalfaqih)
* http://obalfaqih.com
*
* Unity | Imitating the Axe of Kratos
*
* This is the first tutorial of the new series: Inspired by Games
* Where we will choose a feature from our favorite games
* and try to copy it and learn the science behind it
*
* Full tutorial available at:
* https://www.youtube.com/watch?v=PxdoBJBCcrw
*/

/*
* How to use:
* 1- Attach this script to the player, or the parent of your axe
* 2- Make sure that your axe has Rigidbody component on it
* 3- You will have a target which the handler of the axe, or where the axe should be when the player is holding it
* 4- Create an empty game object which should stay attached to the hander (target) and place it next to it. It is the
*    point that gives the curve to your axe returning movement. Keep adjusting it until you find the perfect curve.
*/

public class ThrowableAxe : MonoBehaviour {

    // The axe object
    public Rigidbody axe;
    // Amount of force to apply when throwing
    public float throwForce = 50;
    // the target; which is the player's hand.
    public Transform target
    // The middle point between the axe and the player's hand, to give it a curve
    public Transform curve_point;
    // Last position of the axe before returning it, to use in the Bezier Quadratic Curve formula
    private Vector3 old_pos;
    // Is the axe returning? To update the calculations in the Update method
    private bool isReturning = false;
    // Timer to link to the Bezier formual, Beginnning = 0, End = 1
    private float time = 0.0f;
	
	// Update is called once per frame
	void Update () {
        // When the user/player hits the mouse left button
        if (Input.GetButtonUp("Fire1"))
        {
            ThrowAxe();
        }

        // When the user/player hits the mouse right button
        if (Input.GetButtonUp("Fire2"))
        {
            ReturnAxe();
        }

        // If the axe is returning
        if(isReturning){
            // Returning calcs
            // Check if we haven't reached the end point, where time = 1
            if(time < 1.0f){
                // Update its position by using the Bezier formula based on the current time
                axe.position = getBQCPoint(time, old_pos, curve_point.position, target.position);
                // Reset its rotation (from current to the targets rotation) with 50 units/s
                axe.rotation = Quaternion.Slerp(axe.transform.rotation, target.rotation, 50 * Time.deltaTime);
                // Increase our timer, if you want the axe to return faster, then increase "time" more
                // With something like:
                // time += Timde.deltaTime * 2;
                // It will return as twice as fast
                time += Time.deltaTime;
            }else{
                // Otherwise, if it is 1 or more, we reached the target so reset
                ResetAxe();
            }
        }
	}

    // Throw axe
    void ThrowAxe(){
        // The axe isn't returning
        isReturning = false;
        // Deatach it form its parent
        axe.transform.parent = null;
        // Set isKinematic to false, so we can apply force to it
        axe.isKinematic = false;
        // Add force to the forward axis of the camera
        // We used TransformDirection to conver the axis from local to world
        axe.AddForce(Camera.main.transform.TransformDirection(Vector3.forward) * throwForce, ForceMode.Impulse);
        // Add torque to the axe, to give it much cooler effect (rotating)
        axe.AddTorque(axe.transform.TransformDirection(Vector3.right) * 100, ForceMode.Impulse);
    }

    // Return axe
    void ReturnAxe(){
        // We are returning the axe; so it is in its first point where time = 0
        time = 0.0f;
        // Save its last position to refer to it in the Bezier formula
        old_pos = axe.position;
        // Now we are returning the axe, to start the calculations in the Update method
        isReturning = true;
        // Reset its velocity
        axe.velocity = Vector3.zero;
        // Set isKinematic to true, so now we control its position directly without being affected by force
        axe.isKinematic = true;
    }

    // Reset axe
    void ResetAxe(){
        // Axe has reached, so it is not returning anymore
        isReturning = false;
        // Attach back to its parent, in this case it will attach it to the player (or where you attached the script to)
        axe.transform.parent = transform;
        // Set its position to the target's
        axe.position = target.position;
        // Set its rotation to the target's
        axe.rotation = target.rotation;
    }

    // Bezier Quadratic Curve formula
    // Learn more:
    // https://en.wikipedia.org/wiki/B%C3%A9zier_curve
    Vector3 getBQCPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2){
        // "t" is always between 0 and 1, so "u" is other side of t
        // If "t" is 1, then "u" is 0
        float u = 1 - t;
        // "t" square
        float tt = t * t;
        // "u" square
        float uu = u * u;
        // this is the formula in one line
        // (u^2 * p0) + (2 * u * t * p1) + (t^2 * p2)
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }
}
