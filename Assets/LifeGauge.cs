using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{

    public List<GameObject> Life = new List<GameObject>();



public void tresvidas()
    { 
            Life[0].GetComponent < Image >().enabled = true;
            Life[1].GetComponent < Image >(). enabled = true;
            Life[2].GetComponent < Image >(). enabled = true;
        }

public void duasvidas()
        {
            Life[0].GetComponent<Image>().enabled = true;
            Life[1].GetComponent<Image>().enabled = true;
            Life[2].GetComponent<Image>().enabled = false;
        }

public void umavida()
        {
            Life[0].GetComponent<Image>().enabled = true;
            Life[1].GetComponent<Image>().enabled = false;
            Life[2].GetComponent<Image>().enabled = false;
        }
    
}
