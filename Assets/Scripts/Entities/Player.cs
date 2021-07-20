using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Mover), typeof(GroundDetection))]
public class Player : MonoBehaviour
{
    [SerializeField] private TriggerDetection _triggerDetection = null;
    
    private GameConfig _config = null;
    private Mover _mover = null;
    private GroundDetection _groundDetector = null;
    private int _score = 0;
    private UserInput _userInput = null;

    public event Action<int> OnScoreChanged = null;
    public event Action OnLose = null; 

    private void Awake() =>
        _groundDetector = GetComponent<GroundDetection>();

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
    
    [Inject]
    private void Construct(UserInput userInput, GameConfig config)
    {
        _userInput = userInput;
        _config = config;
        
        _userInput.OnPress += EnableMover;
        _mover = GetComponent<Mover>();
        _mover.SetSpeed(_config.Speed);
    }

    private void EnableMover()
    {
        _mover.enabled = true;
        _userInput.OnPress -= EnableMover;
        _userInput.OnPress += ChangeDirection;
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
            _userInput.OnPress -= ChangeDirection;
        }
    }

    private void IncreaseScore(int amount)
    {
        _score += amount;
        OnScoreChanged?.Invoke(_score);
    }
}