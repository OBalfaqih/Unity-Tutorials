/* 
* Created by: Omar Balfaqih (@OBalfaqih)
* http://obalfaqih.com
*
* Unity | Dead Eye - Red Dead Redemption (Inspired by Games)
*
* This is the second episode of the new series: Inspired by Games
* Where we will choose a feature from our favorite games
* and try to copy it and learn the science behind it
*
* Full tutorial available at:
* https://www.youtube.com/watch?v=LofJMnWPClo&index=2&list=PLaqp5z-4pFi5auiUbsq_KChZKX-DufAOI
*/

/*
* How to use:
* 1- Attach this script to the camera
* 2- Create multiple UI Crosses (the "x" target ui) and assign them to the "cross" variable
* 3- Change the "CameraLook" to your camera look script, in this case it is CameraLook, if you're using it then leave it as it is.
*    Then assign the camera script to it.
* 4- Create a PostProcessingLayer and PostProcessingVolume then assign the volume to the "ppv" variable.
* 5- Add an AudioSource component and assign the gun shot sound, you can find it in "SFX" folder or you can use your own.
*    Assign it to "shot_sfx" variable.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Using the PostProcessing library, to control the PostProcessingVolume (ppv)
using UnityEngine.Rendering.PostProcessing;

public class DeadEyeSystemFinished : MonoBehaviour {
    // Three different states of DeadEye [off: not using DeadEye, targeting: Aiming in DeadEye mode,
    // shooting: Auto-Shoot at targets (player won't have control over looking around and shooting)]
    public enum DeadEyeState
    {
        off,
        targeting,
        shooting
    };
    // Create an instance of DeadEyeState as a variable
    private DeadEyeState deadEyeState = DeadEyeState.off;

    // List of assigned targets
    private List<Transform> targets;
    // An array of the cross UI, the small "x" indicator for the targets
    public Transform[] cross;

    // Your camera script, if you're using another one, simply change the class name to yours
    public CameraLook camLook;
    // Storing the PostProcessVolume component to adjust its weight (On/Off)
    public PostProcessVolume ppv;

    // The animator component of your gun
    public Animator anim;
    // Timer for the gun to cooldown, you can link it to your current gun's script
    private float cooldownTimer = 0;
    // The audio source that contains the gun shot sound
    public AudioSource shot_sfx;

    private void Update()
    {
        // Update timer
        if (cooldownTimer > 0.0f)
            cooldownTimer -= Time.deltaTime;
        else
            cooldownTimer = 0.0f;

        // Aim (Hold Right Click) - Enter DeadEye
        if (Input.GetButtonDown("Fire2"))
        {
            // Enter targeting mode if it's off
            if (deadEyeState == DeadEyeState.off)
            {
                deadEyeState = DeadEyeState.targeting;
            }
        }

        // Fire (Left Click) - If DeadEye, SetTarget. Else just Fire()
        if (Input.GetButtonDown("Fire1"))
        {
            // If we're not in the DeadEye mode, fire a single shot
            if (deadEyeState == DeadEyeState.off)
                Fire();

            // If we're in targeting state in the DeadEye mode, then we will assign a new target
            if (deadEyeState == DeadEyeState.targeting)
            {
                // Setting targets
                // Storing the collision info into a new variable "hit"
                RaycastHit hit;
                // Check if we hit an object starting from our position, going straight forward with a max distance of 100 units
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100))
                {
                    // Creating a temporary GameObject to store the target info
                    GameObject tmpTarget = new GameObject();
                    // Assign the target position to it
                    tmpTarget.transform.position = hit.point;
                    // Attach it to the target (child of the target) so it updates its position if the target is moving
                    tmpTarget.transform.parent = hit.transform;
                    // Add its transform to our List of targets
                    targets.Add(tmpTarget.transform);
                }
            }
        }

        // Release (Release Right Click)
        if (Input.GetButtonUp("Fire2"))
        {
            // If we're in 'targeting' mode, we will go to 'shooting' mode
            if (deadEyeState == DeadEyeState.targeting)
                deadEyeState = DeadEyeState.shooting;
        }
    }

    private void FixedUpdate()
    {
        // Enable/Disable the camera script, update timescale (slow-motion) and turn on/off the post processing effect
        UpdateState();
        // Update the small target UI indicators.
        UpdateTargetUI();
    }

    private void UpdateState()
    {
        // Reset if DeadEye is off
        if (deadEyeState == DeadEyeState.off)
        {
            // Enable the camera script
            camLook.enabled = true;
            // Reset time back to normal
            Time.timeScale = 1;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            // Deactivate the post processing effect
            if (ppv.weight > 0)
                ppv.weight -= Time.deltaTime * 2;
        }
        // When we're in shooting mode
        else if (deadEyeState == DeadEyeState.shooting)
        {
            // Reset time
            Time.timeScale = 1;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            // Disable the camera controls
            camLook.enabled = false;
            // Updating looking at targets and shooting
            UpdateDeadEye();
        }
        // We're in targeting mode
        else
        {
            // Slow-motion
            Time.timeScale = 0.3f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            // Enable camera script
            camLook.enabled = true;
            // Show the post processing effect (Colors and feel of the DeadEye mode)
            if (ppv.weight < 1)
                ppv.weight += Time.deltaTime * 2;
        }
    }

    private void UpdateTargetUI()
    {
        // Loop through all "cross" target indicators
        for (int i = 0; i < cross.Length; i++)
        {
            // If we are still within the targets we have
            if (i < targets.Count)
            {
                // Activate it
                cross[i].gameObject.SetActive(true);
                // Then update its position to the screen position of the target
                cross[i].position = Camera.main.WorldToScreenPoint(targets[i].position);
            }
            else // If we exceeded the last target
                cross[i].gameObject.SetActive(false); //Deactivate it
        }
    }

    private void Fire()
    {
        // Here you can integrate it with your weapon script "myGunScript.Fire()"
        // For this tutorial's scope, we just triggered the animation and played the shot sound
        // To indicate a gun shot

        // Trigger "Shot1" parameter in the animator
        anim.SetTrigger("Shot1");
        // 1.0f / bullets per second to get the cooldown timer between each shot
        cooldownTimer = 1.0f / 2.0f;
        // Play the gun shot sfx
        shot_sfx.Play();
    }

    private void UpdateDeadEye()
    {
        // If we're in shooting state and we still have targets in our list
        if (deadEyeState == DeadEyeState.shooting && targets.Count > 0)
        {
            // Get the current target in a temporary variable of type Transform which is the first element in the list
            Transform currentTarget = targets[0];
            // Get the required rotation for our camera to be looking at the target
            Quaternion rot = Quaternion.LookRotation(currentTarget.position - transform.position);
            // Updating the camera rotation to the "Looking at target" rotation gradually, 30deg/s
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 30 * Time.deltaTime);
            // Get the difference between our current rotation and the target's
            float diff = (transform.eulerAngles - rot.eulerAngles).magnitude;
            // Check if the diff is less than or quals "0.1f" (You can change it depending on the desired accuracy)
            // AND the gun has cooled down (You can use your gun script's cooldown if you are using one)
            if (diff <= 0.1f && cooldownTimer <= 0)
            {
                // We are looking at the target
                // Fire a single shot
                Fire();
                // Remove the target form the list
                targets.Remove(currentTarget);
                // Destroy the target
                Destroy(currentTarget.gameObject);
            }
        }
        else // Either we're not in shooitng mode or we ran out of targets
            deadEyeState = DeadEyeState.off; // Reset the DeadEye state to off
    }
}
