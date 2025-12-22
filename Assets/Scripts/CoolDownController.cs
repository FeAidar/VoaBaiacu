using UnityEngine;

public class CoolDownController: MonoBehaviour
{
    private float _cooldown = 0.15f;
    private float _lastTouchTime;

    public bool CanBeTouched()
    {
        if (Time.time - _lastTouchTime < _cooldown)
            return false;

        _lastTouchTime = Time.time;
        return true;
    }
}