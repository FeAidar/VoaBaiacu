using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private EdgeCollider2D _collider;
    private DrawManager _drawmanager;

    public List<Vector2> _points = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        _collider.transform.position -= transform.position;
        _drawmanager = FindObjectOfType<DrawManager>();
        _drawmanager.linha++;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
            if(_points.Count <2)
            {
                TiraLinha();
            }

    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos))
            return;



       
            _points.Add(pos);
          _renderer.positionCount++;

            _renderer.SetPosition(_renderer.positionCount - 1, pos);

            _collider.points = _points.ToArray();




    }

    private bool CanAppend(Vector2 pos)
    {
        if (_renderer.positionCount == 0)
            return true;
        
        return Vector2.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos) > DrawManager.RESOLUTION;
    }

    public void TiraLinha()
    {
        _drawmanager.linha--;
        Destroy(gameObject);
    }
}
