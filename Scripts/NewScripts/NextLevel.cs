using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string levelToLoad;
    private GameObject player;
    private Transform playerTrans;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerTrans = player.transform;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene(levelToLoad);
            playerTrans.position = new Vector3(0f, -0.5f, -2);
        }
    }

    private void Update()
    {
        Skip();
    }

    void Skip()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(levelToLoad);
            playerTrans.position = new Vector3(0f, -0.5f, -2);
        }
    }
}
