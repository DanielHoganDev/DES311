using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonSound : MonoBehaviour
{
    bool nGhasPlayed;
    bool ohasPlayed;
    bool ehasPlayed;
    bool mvhasPlayed;
    bool bhasPlayed;
    bool dvhasPlayed;
    bool mhasPlayed;
    bool sfxhasPlayed;


    // Start is called before the first frame update
    void Start()
    {
        nGhasPlayed = false;
        ohasPlayed = false;
        ehasPlayed = false;
        mvhasPlayed = false;
        bhasPlayed = false;
        dvhasPlayed = false;
        mhasPlayed = false;
        sfxhasPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void newGameEnter()
    {
            if (nGhasPlayed == false)
            {
                AkSoundEngine.PostEvent("New_Game", gameObject);
                nGhasPlayed = true;
            }

    }

    public void newGameExit()
    {
        nGhasPlayed = false;
    }

    public void optionsEnter()
    {
        if (ohasPlayed == false)
        {
            AkSoundEngine.PostEvent("Options", gameObject);
            ohasPlayed = true;
        }

    }

    public void optionsExit()
    {
        ohasPlayed = false;
    }

    public void exitEnter()
    {
        if (ehasPlayed == false)
        {
            AkSoundEngine.PostEvent("Exit", gameObject);
            ehasPlayed = true;
        }

    }

    public void exitExit()
    {
        ehasPlayed = false;
    }

    public void musicvolEnter()
    {
        if (mvhasPlayed == false)
        {
            AkSoundEngine.PostEvent("Music_Vol", gameObject);
            mvhasPlayed = true;
        }

    }

    public void musicvolExit()
    {
        mvhasPlayed = false;
    }

    public void backEnter()
    {
        if (bhasPlayed == false)
        {
            AkSoundEngine.PostEvent("Back", gameObject);
            bhasPlayed = true;
        }

    }

    public void backExit()
    {
        bhasPlayed = false;
    }

    public void dvVolEnter()
    {
        if (dvhasPlayed == false)
        {
            AkSoundEngine.PostEvent("Dialogue_Vol", gameObject);
            dvhasPlayed = true;
        }

    }

    public void dvVolExit()
    {
        dvhasPlayed = false;
    }

    public void mVolEnter()
    {
        if (mhasPlayed == false)
        {
            AkSoundEngine.PostEvent("Master_Vol", gameObject);
            mhasPlayed = true;
        }

    }

    public void mVolExit()
    {
        mhasPlayed = false;
    }

    public void sfxVolEnter()
    {
        if (sfxhasPlayed == false)
        {
            AkSoundEngine.PostEvent("SFX_Vol", gameObject);
            sfxhasPlayed = true;
        }

    }

    public void sfxVolExit()
    {
        sfxhasPlayed = false;
    }
}
