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
          playerSettings.Rigidbody2D.AddForce (transform.up*1.2f, ForceMode2D.Impulse);

            
           
        }

        if (collision.gameObject.TryGetComponent(out SpawnableObject spawnableObject))
        {
            RemoveLine();
            spawnableObject.Rigidbody2D.AddForce(transform.up * 1.1f, ForceMode2D.Impulse);



        }
    }

    void RemoveLine()
    {
        if (_cancel) return;
        _cancel = true;
        line.ReturnToPool();
    }
}
