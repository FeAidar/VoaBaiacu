using System.Collections.Generic;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D edgeCollider2D;
    private float _timeLeft;
    private DrawManager _drawManager;

    private float _distance;
    public float Distance => _distance;

    private List<Vector2> points = new List<Vector2>();
    private bool _start;

    public void SetPool(DrawManager manager)
    {
        _drawManager = manager;
    }

    public void ResetLine()
    {
        _start = true;
        _distance = 0f;

        points.Clear();

        // RESET CORRETO
        lineRenderer.positionCount = 0;
        edgeCollider2D.points = System.Array.Empty<Vector2>();
        edgeCollider2D.enabled = true;
    }

    void Update()
    {
        if (!_start)
            return;
        

        // 🔒 PROTEÇÃO ABSOLUTA
        if (points.Count < 2)
        {
            _distance = 0f;

            if (Input.GetMouseButtonUp(0))
                ReturnToPool();

            return;
        }

        int mid = points.Count / 2;
        _distance =
            Vector2.Distance(points[0], points[mid]) +
            Vector2.Distance(points[mid], points[^1]);
        
        if (_timeLeft < _drawManager.LineDuration)
        {
            _timeLeft+= Time.deltaTime;
        }
        else
        {
            ReturnToPool();
        }
    }

    public void SetPosition(Vector2 worldPos)
    {
        if (!CanAppend(worldPos))
            return;

        points.Add(worldPos);

        // LineRenderer (world space)
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, worldPos);

        // EdgeCollider2D precisa de 2 pontos no mínimo
        Vector2[] localPoints;

        if (points.Count == 1)
        {
            Vector2 p = transform.InverseTransformPoint(points[0]);
            localPoints = new Vector2[] { p, p };
        }
        else
        {
            localPoints = new Vector2[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                localPoints[i] = transform.InverseTransformPoint(points[i]);
            }
        }

        edgeCollider2D.points = localPoints;

      
    }



    private bool CanAppend(Vector2 pos)
    {
        if (points.Count == 0)
            return true;

        return Vector2.Distance(points[^1], pos) > DrawManager.Resolution;
    }

    public void ReturnToPool()
    {
        _drawManager.RemoveCurrentLine(this);
        _timeLeft = 0f;
        _start = false;
        edgeCollider2D.enabled = false;
        int linePoints = lineRenderer.positionCount;
        DOTween.To(()=> linePoints, x=> linePoints = x, 0, 0.15f).OnUpdate(()=> lineRenderer.positionCount = linePoints).OnComplete(() =>
        {
            _drawManager.ChangeLinesAmount(-1);
            _drawManager.ReturnLineToPool(this);
            
        });
   
    }
}
