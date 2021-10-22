using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    GameObject ImagemLigado;
    [SerializeField]
    GameObject ImagemDesligado;
    private int muted;
    void Start()

    {

        if (PlayerPrefs.HasKey("muted"))
        {
            if (PlayerPrefs.GetInt("muted") == 1)

            {
                muted = 1;
                Debug.Log("Mutadas");
                AudioListener.pause = true;
            }

        }
        Load();
    }

    private void UpdateButtonIcon()
    {
        if (muted == 0)
        {
            this.GetComponent<Image>().sprite = ImagemLigado.GetComponent<Image>().sprite;
        }
        else
        {
            this.GetComponent<Image>().sprite = ImagemDesligado.GetComponent<Image>().sprite;
        }
    }

    public void OnButtonPress()
    {
        if (muted == 0)
        {
            muted = 1;
            AudioListener.pause = true;
        }
        else
        {
            muted = 0;
            AudioListener.pause = false;
        }
        UpdateButtonIcon();
        Save();
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted");
        if (muted == 0)
        {
            this.GetComponent<Image>().sprite = ImagemLigado.GetComponent<Image>().sprite;

        }
        else
        {
            this.GetComponent<Image>().sprite = ImagemDesligado.GetComponent<Image>().sprite;
        }



    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted);


    }



}
