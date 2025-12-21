using System.Collections;
using UnityEngine;


public class UpScreenSpawner : Spawner
{

    [Header("Spawn Settings")]
    [SerializeField] private float verticalOffset = 0f;

    // Gizmo support
    private float _lastHalfWidth;
    private float _lastSpawnY;
    
    
    protected void Spawn()
    {
        GameObject gObject = _pool.Get(); 
        PrepareTransform(gObject);
        PlaceAboveScreen(gObject);

    }

    // Ensures the object is in a neutral state before measuring bounds
    private void PrepareTransform(GameObject gObject)
    {
        gObject.transform.position = Vector3.zero;
        gObject.transform.rotation = Quaternion.identity;
        gObject.transform.localScale = Vector3.one;

        Physics.SyncTransforms();
    }

    // Calculates a valid position above the visible screen
    private void PlaceAboveScreen(GameObject gObject)
    {
        if (gObject.TryGetComponent(out SpawnableObject spawnable))
        {
            Renderer render = spawnable.MainRenderer;

            Vector3 size = render.bounds.size;
            Vector3 halfSize = size * 0.5f;

            float minX = _bottomLeft.x + halfSize.x;
            float maxX = _topRight.x - halfSize.x;

            float x = Random.Range(minX, maxX);
            float y = _topRight.y + halfSize.y + verticalOffset;

            // Cache values for gizmo
            _lastHalfWidth = halfSize.x;
            _lastSpawnY = y;

            gObject.transform.position = new Vector3(x, y, 0f);
        }

       
    }
    
    protected override IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minWaitTime, _maxWaitTime));
            Spawn();
        }
    }

    // Draws the horizontal spawn line above the screen
    private void OnDrawGizmos()
    {
        if (_lastHalfWidth <= 0f)
        {
            if (AssignedHazard.hazard.TryGetComponent(out SpawnableObject spawnable))
            {
                Renderer render = spawnable.MainRenderer;
                Vector3 size = render.bounds.size;
                Vector3 halfSize = size * 0.5f;
                float y = _topRight.y + halfSize.y + verticalOffset;
                _lastHalfWidth = halfSize.x;
                _lastSpawnY = y;

            }
            else return;

        }

        float minX = _bottomLeft.x + _lastHalfWidth;
        float maxX = _topRight.x - _lastHalfWidth;
        float width = maxX - minX;
        Vector3 center = new Vector3(
            (minX + maxX) * 0.5f,
            _lastSpawnY,
            0f
        );
        float thickness = 0.2f;
        Color color = Color.cyan;
        color.a = 0.5f;
        Gizmos.color = color;
        Gizmos.DrawCube(center, new Vector3(width, thickness, 0f));
    }


}
    
