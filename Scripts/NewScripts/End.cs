using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public void MainMenu()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene("Main Menu");
    }
}
