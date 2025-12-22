using UnityEngine;

public class LineColliderManager : MonoBehaviour
{

    private bool _cancel;
    [SerializeField] private Line line;

    private void OnEnable()
    {
        _cancel = false;
    }
   
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerSettings playerSettings))
        { 
            RemoveLine();
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 dir = -contact.normal;
            Vector2 finalDir = (dir + Vector2.up).normalized;
            playerSettings.Rigidbody2D.AddForce(finalDir * 0.7f, ForceMode2D.Impulse);
        

            
           
        }

        if (collision.gameObject.TryGetComponent(out SpawnableObject spawnableObject))
        {
            RemoveLine();
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 dir = -contact.normal;
            Vector2 finalDir = (dir + Vector2.up).normalized;
            spawnableObject.Rigidbody2D.AddForce(finalDir* 0.7f, ForceMode2D.Impulse);



        }
    }

    void RemoveLine()
    {
        if (_cancel) return;
        _cancel = true;
        line.ReturnToPool();
    }
}
