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
    private UserInput _userInput = null;

    public event Action<int> OnScoreChanged = null;
    public event Action OnLose = null; 

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _mover.SetSpeed(_config.Speed);
        
        _groundDetector = GetComponent<GroundDetection>();
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
    
    public void Construct(UserInput userInput)
    {
        _userInput = userInput;
        
        _userInput.OnPress += EnableMover;
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