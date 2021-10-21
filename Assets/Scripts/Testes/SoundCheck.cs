using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCheck : MonoBehaviour
{

    void Start()
    {
        if (PlayerPrefs.HasKey("muted"))
        {
            if (PlayerPrefs.GetInt("muted") == 1)

            {

                Debug.Log("Mutado");
                AudioListener.pause = true;
            }
            else
                AudioListener.pause = false;

        }
        else
            AudioListener.pause = false;
    }


}

