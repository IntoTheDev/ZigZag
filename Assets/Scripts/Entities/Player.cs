using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(GroundDetection))]
public class Player : MonoBehaviour
{
    [SerializeField] private TriggerDetection _triggerDetection = null;
    [SerializeField] private GameConfig _config = null;

    private Mover _mover = null;
    private GroundDetection _groundDetector = null;
    private int _score = 0;
    private readonly WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

    public static event Action<int> OnScoreChanged = null;
    public static event Action OnLose = null; 

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _mover.SetSpeed(_config.Speed);
        
        _groundDetector = GetComponent<GroundDetection>();
        UserInput.OnPress += EnableMover;
    }

    private void OnEnable()
    {
        _triggerDetection.OnEnter += OnDetected;
        _groundDetector.OnStateChanged += OnGroundChange;
    }

    private void OnDisable()
    {
        _triggerDetection.OnEnter -= OnDetected;
        _groundDetector.OnStateChanged -= OnGroundChange;
    }

    private void EnableMover()
    {
        _mover.enabled = true;
        UserInput.OnPress -= EnableMover;
        UserInput.OnPress += ChangeDirection;
    }
    
    private void ChangeDirection()
    {
        _mover.ChangeDirection();
        IncreaseScore(1);
    }
    
    private void OnDetected(Collider other) =>
        IncreaseScore(other.GetComponent<Pickupable>().Take());

    private void OnGroundChange(bool grounded)
    {
        if (!grounded)
        {
            enabled = false;
            OnLose?.Invoke();
        }
    }

    private void IncreaseScore(int amount)
    {
        _score += amount;
        OnScoreChanged?.Invoke(_score);
    }
}