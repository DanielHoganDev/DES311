using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolController : MonoBehaviour
{
    //Opens a plug bit to specify the slider
    public Slider thisSlider;
    //Wwise likes having multiple instad of one float for all
    public float masterVol;
    public float musicVol;
    public float sfxVol;
    public float dialogueVol;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Updates the volume sliders
    public void SetSpecificVolume(string whatValue)
    {
        float sliderValue = thisSlider.value;

        //Affects the master volume
        if (whatValue == "Master")
        {
            masterVol = thisSlider.value;
            //Calls wwise to set the Master vol RTPC val to the master vol slider value
            AkSoundEngine.SetRTPCValue("MasterVol", masterVol);
        }

        //Affects the Music volume
        if (whatValue == "Music")
        {
            musicVol = thisSlider.value;
            AkSoundEngine.SetRTPCValue("MusicVol", musicVol);
        }

        //Affects the sfx volume
        if (whatValue == "SFX")
        {
            sfxVol = thisSlider.value;
            AkSoundEngine.SetRTPCValue("SFXVol", sfxVol);
        }

        //Affects the dialogue volume
        if (whatValue == "Dialogue")
        {
            dialogueVol = thisSlider.value;
            AkSoundEngine.SetRTPCValue("DialogueVol", dialogueVol);
            AkSoundEngine.PostEvent("Dialogue_Vol", gameObject);
        }
    }
}
