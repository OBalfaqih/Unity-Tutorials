using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    public void goToScene(string sceneName)
    {
    	// Loading the passed scene name
        SceneManager.LoadScene(sceneName);
    }
}