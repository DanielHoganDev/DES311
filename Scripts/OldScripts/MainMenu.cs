using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject music;
    GameObject myEventSystem;

    public void Start()
    {
        GameObject myEventSystem = GameObject.Find("EventSystem");
    }

    //Will load game scene.
    public void playGame()
    {
        SceneManager.LoadScene("1st Level");
    }

    //Sets the options menu as the active menu.
    public void options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        if(mainMenu.activeInHierarchy == false)
        {
            
        }
    }

    //Sets the main menu as the active menu.
    public void back()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    //Will quit the game.
    public void exitGame()
    {
        Application.Quit();
    }

}
