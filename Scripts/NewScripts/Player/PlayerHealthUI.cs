using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthUI : HealthUI
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    public override void Update()
    {
        slider.maxValue = player.GetComponent<PlayerStats>().maxHealth.GetValue();
    }
}
