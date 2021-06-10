using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(GroundDetection))]
public class Player : MonoBehaviour
{
    [SerializeField] private TriggerDetection _triggerDetection = null;

    private Mover _mover = null;
    private GroundDetection _groundDetector = null;
    private bool _wasPressed = false;
    private int _score = 0;
    private WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

    public static event Action<int> OnScoreChanged = null;
    public static event Action OnTap = null;
    public static event Action OnLose = null; 

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _groundDetector = GetComponent<GroundDetection>();

        StartCoroutine(WaitForTap());
    }

    private void OnEnable()
    {
        _triggerDetection.OnEnter += OnDetected;
        _groundDetector.OnStateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        _triggerDetection.OnEnter -= OnDetected;
        _groundDetector.OnStateChanged -= OnStateChanged;
    }

    private void Update()
    {
#if !UNITY_EDITOR
        foreach (var touch in Input.touches)
        {
            if (touch.fingerId == 0 && touch.phase == TouchPhase.Began)
            {
                OnTap?.Invoke();
                _wasPressed = true;
            }

        }
#else
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnTap?.Invoke();
            _wasPressed = true;
        }
#endif

        if (!_mover.isActiveAndEnabled)
            return;

        if (_wasPressed)
        {
            _mover.ChangeDirection();
            IncreaseScore(1);
            _wasPressed = false;
        }
    }

    private IEnumerator WaitForTap()
    {
        while (!_wasPressed)
        {
            _waitForEndOfFrame = new WaitForEndOfFrame();
            yield return _waitForEndOfFrame;
        }

        _wasPressed = false;
        _mover.enabled = true;
    }

    private void OnDetected(Collider other) =>
        IncreaseScore(other.GetComponent<Crystal>().Take());

    private void OnStateChanged(bool state)
    {
        if (!state)
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