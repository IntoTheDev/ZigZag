using UnityEngine;

public class DirectionalMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Vector3 _direction = default;

    private Transform _transform = null;

    private void Awake() =>
        _transform = transform;

    private void Update() =>
        _transform.localPosition += _direction * (_speed * Time.deltaTime);
}
