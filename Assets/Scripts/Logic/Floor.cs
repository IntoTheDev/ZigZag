using ToolBox.Pools;
using UnityEngine;

[RequireComponent(typeof(DirectionalMover))]
public class Floor : MonoBehaviour, IPoolable
{
    [SerializeField] private TriggerDetection _triggerDetection = null;
    
    private DirectionalMover _mover = null;
    private const float TIME_BEFORE_FALL = 1f;
    private const float TIME_BEFORE_RELEASE = 2f;

    private void Awake() =>
        _mover = GetComponent<DirectionalMover>();

    private void OnEnable() =>
        _triggerDetection.OnExit += OnExit;
    
    private void OnDisable() =>
        _triggerDetection.OnExit -= OnExit;

    private void OnExit(Collider other) =>
        Invoke(nameof(EnableMover), TIME_BEFORE_FALL);

    private void EnableMover()
    {
        _mover.enabled = true;
        Invoke(nameof(Release), TIME_BEFORE_RELEASE);
    }

    private void Release() =>
        gameObject.Release();

    public void OnGet() { }

    public void OnRelease() =>
        _mover.enabled = false;
}
