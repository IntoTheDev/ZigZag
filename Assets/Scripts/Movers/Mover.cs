using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    
    private Vector3 _direction = default;
    private Transform _transform = null;
    
    private void Awake()
    {
        _direction = Vector3.right;
        _transform = transform;
    }

    public void ChangeDirection() =>
        _direction = _direction == Vector3.right ? Vector3.forward : Vector3.right;

    private void Update() =>
        _transform.position += _direction * (_speed * Time.deltaTime);
}
