using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    private float _width;
    private BoxCollider2D _collider;
    public float ScrollSpeed;
    private Rigidbody2D _rb;

    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _width = _collider.size.x;
        _collider.enabled = false;
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(ScrollSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -_width)
        {
            Vector2 resetPosition = new Vector2(_width * 2f, 0);
            transform.position = (Vector2)transform.position + resetPosition;



        }
    }
}
