using System;
using Unity.Collections;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private Line linePrefab;
    private Camera _mainCamera;
    [SerializeField] private int maxSimultaneousLines;
    [SerializeField] private float lineSize;   
    public const float Resolution = 0.1f;
    private Line _currentLine;
    [HideInInspector] public int currentTotalLines;
    private bool _limitReached;
    private bool _checkFingerUp;

    private bool _running;

    private void Awake()
    {
        _mainCamera = Camera.main;
        GameManager.OnStartGame += StartLineManager;
        GameManager.OnEndGame += EndLineManager;
    }

    private void OnDestroy()
    {
        GameManager.OnStartGame -= StartLineManager;
        GameManager.OnEndGame -= EndLineManager;
    }

    void StartLineManager() 
    {
     _running = true;
    }

    void EndLineManager()
    {
        _running = false;
    }
    
    void Update()
    {
        if (!_running) return;
        
        _limitReached = currentTotalLines == maxSimultaneousLines;
        Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (!_limitReached)
            if (Input.GetMouseButtonDown(0))
            {

                _currentLine = Instantiate(linePrefab, mousePos, Quaternion.identity);
                _currentLine.GetReference(this);
                ChangeLinesAmount(1);

            }


        if (Input.GetMouseButton(0))
        {
            if (_limitReached)
            {
                _checkFingerUp = true;
            }
    
            if(_currentLine)
                if (_currentLine.Distance < lineSize)
                        _currentLine.SetPosition(mousePos);
                

            
        }
        

        if(_checkFingerUp)
        {
            if(Input.GetMouseButtonUp(0))
            {
              
                _currentLine = null;
                _checkFingerUp = false;
                
            }
        }

    }

    public void ChangeLinesAmount(int num)
    {
        currentTotalLines += num;
        currentTotalLines = Mathf.Clamp(currentTotalLines, 0, maxSimultaneousLines);
    }
}
