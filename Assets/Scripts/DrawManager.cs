using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private Line linePrefab;
    [SerializeField] private int maxSimultaneousLines;
    [SerializeField] private float lineSize;
    [SerializeField] private float lineDuration = 4f;
    public float LineDuration => lineDuration;

    public const float Resolution = 0.1f;

    private Camera _mainCamera;
    private Line _currentLine;

    [HideInInspector] public int currentTotalLines;

    private bool _limitReached;
    private bool _checkFingerUp;
    private bool _running;

    private Queue<Line> _linePool = new Queue<Line>();

    private void Awake()
    {
        _mainCamera = Camera.main;

        GameManager.OnStartGame += StartLineManager;
        GameManager.OnEndGame += EndLineManager;

        // Pré-instancia o pool
        for (int i = 0; i < maxSimultaneousLines; i++)
        {
            Line line = Instantiate(linePrefab, transform);
            line.gameObject.SetActive(false);
            line.SetPool(this);
            _linePool.Enqueue(line);
        }
    }

    private void OnDestroy()
    {
        GameManager.OnStartGame -= StartLineManager;
        GameManager.OnEndGame -= EndLineManager;
    }

    void StartLineManager() => _running = true;
    void EndLineManager() => _running = false;

    void Update()
    {
        if (!_running) return;

        _limitReached = currentTotalLines == maxSimultaneousLines;
        Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (!_limitReached && Input.GetMouseButtonDown(0))
        {
            _currentLine = GetLineFromPool();
            ChangeLinesAmount(1);
        }

        if (Input.GetMouseButton(0))
        {
            if (_limitReached)
                _checkFingerUp = true;

            if (_currentLine && _currentLine.Distance < lineSize)
                _currentLine.SetPosition(mousePos);
        }

        if (_checkFingerUp && Input.GetMouseButtonUp(0))
        {
            _currentLine = null;
            _checkFingerUp = false;
        }
    }

    private Line GetLineFromPool()
    {
        if (_linePool.Count == 0)
            return null;

        Line line = _linePool.Dequeue();
        line.gameObject.SetActive(true);
        line.ResetLine();
        return line;
    }

    public void ReturnLineToPool(Line line)
    {
        line.gameObject.SetActive(false);
        _linePool.Enqueue(line);
    }

    public void RemoveCurrentLine(Line line)
    {
        if (!_currentLine) return;
        if (_currentLine == line)
        {
            _currentLine = null;
        }
    }

public void ChangeLinesAmount(int num)
    {
        currentTotalLines += num;
        currentTotalLines = Mathf.Clamp(currentTotalLines, 0, maxSimultaneousLines);
    }
}
