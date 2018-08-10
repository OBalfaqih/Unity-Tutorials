using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToManager : MonoBehaviour {

    // The object that contains the Help UI
    public GameObject help;

    private void Start()
    {
        // Check if the key exists
        if(PlayerPrefs.HasKey("FirstTime")){
            // It is not the first time
            // You can set any code you want here if it is not the first time
            // Maybe you can show "Welcome Again" screen
        }else{
            // It is the first
            PlayerPrefs.SetInt("FirstTime", 1);
            showHelp();
        }
    }

    void showHelp(){
        // Setting 'help' active / enabled
        help.SetActive(true);
    }
}