using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
* CameraShake.cs
*
* Created by: Omar Balfaqih (@OBalfaqih)
* http://obalfaqih.com
*
* Making Wingsuit - Far Cry 5 (Inspired by Games) | Unity
*
* This is the third episode of the series: Inspired by Games
* Where we choose a feature from our favorite games
* and try to copy it and learn the science behind it
*
* Full tutorial available at:
* https://www.youtube.com/watch?v=g4YWcklQoVg
*/

/*
* How to use:
* 1- Attach this script to the camera
* 2- Make sure the camera is a child of your player
* 3- Attach the player's 'WingsuitController' script to the 'wc' variable
* 4- You can adjust the 'shaking' variable, which is the value of how much can the camera shake (Optional)
*/

public class CameraShake : MonoBehaviour
{
	// The wingsuit script that the player has
    public WingsuitController wc;
    // The amount of shaking
    public float shaking = 0.5f;

    private void LateUpdate()
    {
    	// Will affect the shaking based on the player's x rotation
        float mod_shaking = shaking * wc.percentage;
        // Give the camera a random position based on the percentage and shaking variables
        transform.localPosition = new Vector3(Random.Range(-mod_shaking, mod_shaking), Random.Range(-mod_shaking, mod_shaking), 0);
    }
}
