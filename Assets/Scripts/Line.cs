using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D edgeCollider2D;
    private DrawManager _drawManager;
    public DrawManager drawManager => _drawManager;
    private float _distance;
    public float Distance => _distance;
    [SerializeField] private List<Vector2> points = new List<Vector2>();
    private bool _start;
    
    
    void Start()
    {
        edgeCollider2D.transform.position -= transform.position;


    }

    public void GetReference(DrawManager drawManager)
    {
        _drawManager = drawManager;
        _start = true;
    }
    void Update()
    {
        if (!_start) return;
        
        int numero = points.Count;
        float distancia1 = Vector2.Distance(points[0], points[(numero - 1) /2]);
        float distancia2 = Vector2.Distance(points[(numero-1)/2], points[(numero - 1)]);
        _distance = distancia1 + distancia2;
        
        if (Input.GetMouseButtonUp(0))
            if(points.Count <2)
            {
                RemoveLineCount();
            }

    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos))
            return;



       
        points.Add(pos);
        lineRenderer.positionCount++;

        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);

        edgeCollider2D.points = points.ToArray();




    }

    private bool CanAppend(Vector2 pos)
    {
        if (lineRenderer.positionCount == 0)
            return true;
        
        return Vector2.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1), pos) > DrawManager.Resolution;
    }

    public void RemoveLineCount()
    {
        _drawManager.ChangeLinesAmount(-1);
        Destroy(gameObject);
    }
}
