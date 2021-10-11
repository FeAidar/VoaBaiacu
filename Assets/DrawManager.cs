using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private Line _linePrefab;

    public const float RESOLUTION = 0.1f;

    private Line _currentLine;
    public int LimiteDeLinhas;
    public int linha;
    private bool naodesenha;
    private bool check;
    public int LimiteDePontos;
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        if (linha != LimiteDeLinhas)
            naodesenha = false;
        else
            naodesenha = true;

        //Debug.Log(linha);


        if (!naodesenha)
            if (Input.GetMouseButtonDown(0))
        {

                _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);
                
   

        }


        if (Input.GetMouseButton(0))
        {
            if (naodesenha)
            {
                check = true;
            }
            if (_currentLine._points.Count <= LimiteDePontos)
            {
                if (_currentLine != null)
                    _currentLine.SetPosition(mousePos);
            }
        }
        

            if(check)
        {
            if(Input.GetMouseButtonUp(0))
            {
              
                _currentLine = null;
                check = false;
            }
        }

    }
}
