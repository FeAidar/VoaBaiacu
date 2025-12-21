
    using System.Collections;
    using UnityEngine;

    public class InScreenSpawner : Spawner
    { 
        [Header("Spawn Settings")]
    [SerializeField] private int maxAttempts = 20;

    [Header("Spawn Margins (World Units)")]
    [SerializeField] private float marginLeft;
    [SerializeField] private float marginRight;
    [SerializeField] private float marginTop;
    [SerializeField] private float marginBottom;

 
   private void Spawn()
    { 
        GameObject gObject = _pool.Get(); 
        PrepareTransform(gObject);
      bool placed = TryPlaceWithoutOverlap(gObject);
      if (!placed)
        {
           _pool.Release(gObject);
           
        }

        
    }
    // Ensures the object is in a neutral state before measuring bounds
    private void PrepareTransform(GameObject gObject)
    {
        gObject.transform.position = Vector3.zero;
        gObject.transform.rotation = Quaternion.identity;
        gObject.transform.localScale = Vector3.one;
        
    }
    protected override IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minWaitTime, _maxWaitTime));
            Spawn();
        }
    }
   

    // Tries multiple times to find a valid position
    private bool TryPlaceWithoutOverlap(GameObject gObject)
    {
        
        if (!gObject.TryGetComponent(out SpawnableObject obj)) return false;
        Renderer rend = obj.MainRenderer;
          
// Force renderer bounds to update after transform reset
        rend.enabled = false;
        rend.enabled = true;

// Ensure transforms are synced before reading bounds
        Physics.SyncTransforms();

        Vector3 size = rend.bounds.size;

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 candidatePosition = GetRandomSafePosition(size * 0.5f);

            Bounds candidateBounds = new Bounds(candidatePosition, size);

            if (!IntersectsAny(candidateBounds, gObject))
            {
                gObject.transform.position = candidatePosition;
                return true;
            }
        }

        return false;
        
    }

    // Checks overlap against all other active objects under the same parent
    private bool IntersectsAny(Bounds candidateBounds, GameObject current)
    {
        Transform parent = current.transform.parent;
        if (parent == null) return false;

        foreach (Transform child in parent)
        {
            if (child.gameObject == current) continue;
            if (!child.gameObject.activeInHierarchy) continue;

            Renderer otherRenderer = child.GetComponentInChildren<Renderer>();
            if (otherRenderer == null) continue;

            if (candidateBounds.Intersects(otherRenderer.bounds))
                return true;
        }

        return false;
    }


    // Returns a random position where the object will fully fit on screen
    private Vector3 GetRandomSafePosition(Vector2 halfSize)
    {
        Vector3 adjustedBottomLeft = GetAdjustedBottomLeft();
        Vector3 adjustedTopRight = GetAdjustedTopRight();

        float minX = adjustedBottomLeft.x + halfSize.x;
        float maxX = adjustedTopRight.x - halfSize.x;

        float minY = adjustedBottomLeft.y + halfSize.y;
        float maxY = adjustedTopRight.y - halfSize.y;

        if (minX > maxX || minY > maxY)
            return (adjustedBottomLeft + adjustedTopRight) * 0.5f;

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        return new Vector3(x, y, 0f);
    }

    // Applies margins to the bottom-left screen corner
    private Vector3 GetAdjustedBottomLeft()
    {
        return new Vector3(
            _bottomLeft.x + marginLeft,
            _bottomLeft.y + marginBottom,
            0f
        );
    }

    // Applies margins to the top-right screen corner
    private Vector3 GetAdjustedTopRight()
    {
        return new Vector3(
            _topRight.x - marginRight,
            _topRight.y - marginTop,
            0f
        );
    }

    // Draws the spawnable area in the editor
    private void OnDrawGizmos()
    {
        Vector3 bottomLeft = GetAdjustedBottomLeft();
        Vector3 topRight = GetAdjustedTopRight();

        Vector3 center = (bottomLeft + topRight) * 0.5f;
        Vector3 size = topRight - bottomLeft;
        Color color = Color.magenta;
        color.a = 0.5f;
        Gizmos.color = color;
       // Gizmos.DrawWireCube(center, size);
        Gizmos.DrawCube(center, size);
    }
    }
