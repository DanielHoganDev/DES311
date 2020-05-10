using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl instance;

    void Awake()
    {
        //Checks if there is already an instance for the gameobject, if there isn't the gameobject won't be destoryed on load and will create an instance for it.
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        //If there is an instance already it'll destory the new version of the gameobject trying to be created.
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //Checks if the current scene is either the end of the game or the main menu, if the scnene is one of these put the instance back to nothing and destroy the gameobject.
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("End") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Menu"))
        {
            instance = null;
            Destroy(this.gameObject);
        }
    }
}
