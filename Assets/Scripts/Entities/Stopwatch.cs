using UnityEngine;

public class Stopwatch
{
    private readonly float _duration = 0f;
    private float _timestamp = 0f;
    private bool _isElapsed = false;

    public bool IsElapsed 
    {
        get
        {
            // Preventing from using UnityEngine.Time which is quiet expensive performance wise
            if (_isElapsed)
                return true;

            _isElapsed = Time.time - _timestamp > _duration;

            return _isElapsed;
        }
    }
    
    public Stopwatch(float duration) =>
        _duration = duration;

    public void Reset()
    {
        _timestamp = Time.time;
        _isElapsed = false;
    }
}