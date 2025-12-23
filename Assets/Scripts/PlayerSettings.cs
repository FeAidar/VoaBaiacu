
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSettings : MonoBehaviour
{
    
    [Header("Sound Settings")]
    [SerializeField] private MovableSoundEmitter movableSoundEmitter;
    
    [Header("Animation Settings")]
    [SerializeField] private FishAnimations animator;
    [SerializeField] private Collider2D mainCollider;
  
    [Header("Physics Settings")]
    [SerializeField] private Rigidbody2D rb2D;
    public Rigidbody2D Rigidbody2D => rb2D;
    
   
    private GameSettingsSO _gameSettingsSo;
    private int _currentLives;
    private bool _immortal;
    private Vector3 _topRight;
    private Vector3 _bottomLeft;
    public int CurrentLives => _currentLives;
    
    
    private void Awake()
    {
        GameManager.OnStartGame += StartGame;
        GameManager.OnEndGame += Stop;
        GameManager.OnParseSettings += GetSettings;
        ScreenBounds.OnChange += UpdateScreenBounds;
        if (_topRight == Vector3.zero && _bottomLeft == Vector3.zero)
        {
            var bounds = FindObjectOfType<ScreenBounds>();
            if (bounds)
            {
                _topRight = bounds.TopRightCorner;
                _bottomLeft = bounds.DownLeftCorner;
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.OnParseSettings -= GetSettings;
        GameManager.OnStartGame -= StartGame;
        GameManager.OnEndGame -= Stop;
        ScreenBounds.OnChange -= UpdateScreenBounds;
    }
    
   
    
    private void UpdateScreenBounds(Vector3 topRight, Vector3 bottomLeft)
    {
        _topRight = topRight;
        _bottomLeft = bottomLeft;
    }

    private void GetSettings(GameSettingsSO settings)
    {
        _gameSettingsSo = settings;
        _currentLives = _gameSettingsSo.lifeSettings.playerLife;
        LifeEvents.StartGame(_currentLives);
        if (_gameSettingsSo.lifeSettings.playerLife == 0)
        {
            _immortal = true;
        }
    }

    private void StartGame()
    {
        PlaceAtStart();
        movableSoundEmitter.PlayAudio(movableSoundEmitter.AppearAudioClips);
        rb2D.constraints = RigidbodyConstraints2D.None;
        animator.Animate(true);
     
    }
    
    private void PlaceAtStart()
    {
        
        PrepareTransform();
        PlaceAboveScreen();

    }

    // Ensures the object is in a neutral state before measuring bounds
    private void PrepareTransform()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        Physics.SyncTransforms();
    }

    // Calculates a valid position above the visible screen
    private void PlaceAboveScreen()
    {
       
        Vector3 size = mainCollider.bounds.size;
        Vector3 halfSize = size * 0.5f;
        float y = _topRight.y + halfSize.y;
        transform.position = new Vector3(0, y, 0f);
    }

    private void Stop()
    {
        rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.Animate(false);
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<CoolDownController>(out var cd))
        {
            if (!cd.CanBeTouched()) return;
            if (CheckLine(other.gameObject)) return;
            if (CheckWater(other.gameObject)) return;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<CoolDownController>(out var cd))
        {
            if (!cd.CanBeTouched()) return;
            if (CheckSpawnable(other.gameObject)) ;


        }
    }

    private bool CheckSpawnable(GameObject other)
    {
       
        if (other.TryGetComponent<WinHitPoint>(out var winHitPoint))
        {
            Debug.Log("Found a HitPoint!" + other);
            foreach (var score in _gameSettingsSo.scoreSystem.onPlayerTouchingHazards)
            {
                if (score.hazard == winHitPoint.SpawnableObject.HazardType)
                {
                    ScoreEvents.Raise(score.score.score);
                    
                }

            }
            return true;
        }
        return false;
        
        
    }

    private bool CheckLine(GameObject other)
    {
        if (other.TryGetComponent<LineColliderManager>(out var lineColliderManager))
        {
            
            ScoreEvents.Raise(_gameSettingsSo.scoreSystem.onPlayerTouchingLines.score);
                return true;
        }
        return false;
    }

    private bool CheckWater(GameObject other)
    {
        if (other.TryGetComponent<WaterManager>(out var waterManager))
        {
            foreach (var scoreValue in (_gameSettingsSo.scoreSystem.onPlayerTouchingWater))
            {
                if (waterManager.CurrentWaterType == scoreValue.waterType)
                {
                    ScoreEvents.Raise(scoreValue.score.score);
                }
                
            }

            foreach (var damageValue in _gameSettingsSo.lifeSettings.lifeLossByTouchingWater)
            {
                if (waterManager.CurrentWaterType == damageValue.type)
                {
                   CheckLife(damageValue.lifeDamage);
                }
            }

            return true;
        }
        return false;
    }
    
    private void CheckLife(int damageTaken)
    {
        if (_immortal) return;
        _currentLives = Mathf.Max(0, _currentLives + damageTaken);
        LifeEvents.LifeChanged(_currentLives);
        if (_currentLives == 0)
        {
            LifeEvents.GameOver();
        }
       
    }

}
