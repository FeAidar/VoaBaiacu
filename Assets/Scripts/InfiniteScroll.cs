using UnityEngine;
using System;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField] private Transform[] parts;
    [SerializeField] private Vector2 speed;

    [Tooltip("Ajuste fino para eliminar seams. Tipicamente entre 0.0005 e 0.01")]
    [SerializeField] private float overlap = 0.001f;

    public bool unscaled;

    private float width;
    private float halfSpan;
    private int partsLength;

    protected Action<Transform> onScrollPartRecycled;

    void Awake()
    {
        partsLength = parts.Length;

        RecalculateWidth();

        // garante que as partes nascem perfeitamente alinhadas
        for (int i = 0; i < partsLength; i++)
        {
            var pos = parts[i].position;
            pos.x = parts[0].position.x + (i * width);
            parts[i].position = pos;
        }
    }

    void Update()
    {
        float dt = unscaled ? Time.unscaledDeltaTime : Time.deltaTime;
        Vector2 step = speed * dt;

        // recalcula para permitir ajuste ao vivo no Inspector
        RecalculateWidth();

        for (int i = 0; i < partsLength; i++)
        {
            var part = parts[i];
            part.position += (Vector3)step;
            TryRecycle(part);
        }
    }

    void RecalculateWidth()
    {
        var sr = parts[0].GetComponent<SpriteRenderer>();
        var sprite = sr.sprite;

        float baseWidth =
            (sprite.rect.width / sprite.pixelsPerUnit) *
            parts[0].lossyScale.x;

        width = baseWidth - Mathf.Abs(overlap);
        halfSpan = width * partsLength * 0.5f;
    }

    void TryRecycle(Transform part)
    {
        if (speed.x < 0 && part.position.x < -halfSpan)
        {
            part.position += new Vector3(partsLength * width, 0f, 0f);
            onScrollPartRecycled?.Invoke(part);
            return;
        }

        if (speed.x > 0 && part.position.x > halfSpan)
        {
            part.position += new Vector3(-partsLength * width, 0f, 0f);
            onScrollPartRecycled?.Invoke(part);
        }
    }
}
