using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingTutorial : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Check if the key exists
        if (PlayerPrefs.HasKey("PlayerLevel"))
        {
            // If it's there, then load it and store it in a vairable
            int playerLevel = PlayerPrefs.GetInt("PlayerLevel");
            print("Player level is " + playerLevel);
        }else{
            // Otherwise, we will save the value as a new one
            PlayerPrefs.SetInt("PlayerLevel", 4);
            print("Player level was saved successfully");
        }
	}
}
