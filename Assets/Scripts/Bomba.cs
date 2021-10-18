using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{

    public GameObject explosao;
    private bool foi;
    public ScoreGiver _scoregiver;

    private void Start()
    {
        _scoregiver = FindObjectOfType<ScoreGiver>();
    }
    public IEnumerator TiraBomba()
    {
        if (!foi)
        {
            Instantiate(explosao, transform.position, Quaternion.identity);
            foi = true;
            Handheld.Vibrate();
            _scoregiver.StartCoroutine("GivePointBomb");
        }
        yield return new WaitForSecondsRealtime(0.1f);
        Destroy(gameObject);

    }
}
