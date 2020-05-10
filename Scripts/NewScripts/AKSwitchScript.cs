using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AKSwitchScript : MonoBehaviour
{

    [SerializeField]
    private AK.Wwise.Switch cave;
    [SerializeField]
    private AK.Wwise.Switch forest;
    [SerializeField]
    private AK.Wwise.Switch forest2;

    void Update()
    {

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Redesign"))
        {
            forest.SetValue(this.gameObject);
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2"))
        {
            cave.SetValue(this.gameObject);
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3"))
        {
            forest2.SetValue(this.gameObject);
        }

    }
}
