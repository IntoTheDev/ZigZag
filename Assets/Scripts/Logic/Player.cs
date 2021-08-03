using System;
using UnityEngine;
using Zenject;

[DisallowMultipleComponent, RequireComponent(typeof(Mover), typeof(GroundDetection), typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private TriggerDetection _triggerDetection = null;
    
    private GameConfig _config = null;
    private Mover _mover = null;
    private GroundDetection _groundDetector = null;
    private int _score = 0;
    private IUserInput _userInput = null;
    private Rigidbody _body = null;

    public event Action<int> OnScoreChanged = null;
    public event Action OnLose = null; 

    [Inject]
    private void Construct(IUserInput userInput, GameConfig config)
    {
        _userInput = userInput;
        _config = config;
        
        _userInput.OnPress += EnableMover;
        _mover = GetComponent<Mover>();
        _mover.SetSpeed(_config.Speed);
    }
    
    private void Awake()
    {
        _groundDetector = GetComponent<GroundDetection>();
        _body = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _triggerDetection.OnEnter += OnObjectDetected;
        _groundDetector.OnStateChanged += OnGroundStateChanged;
    }

    private void OnDisable()
    {
        _triggerDetection.OnEnter -= OnObjectDetected;
        _groundDetector.OnStateChanged -= OnGroundStateChanged;
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
    
    private void OnObjectDetected(Collider other) =>
        IncreaseScore(other.GetComponent<Pickupable>().Take());

    private void OnGroundStateChanged(bool isGrounded)
    {
        if (!isGrounded)
        {
            enabled = false;
            OnLose?.Invoke();
            _userInput.OnPress -= ChangeDirection;
            _body.useGravity = true;
        }
    }

    private void IncreaseScore(int amount)
    {
        _score += amount;
        OnScoreChanged?.Invoke(_score);
    }
}