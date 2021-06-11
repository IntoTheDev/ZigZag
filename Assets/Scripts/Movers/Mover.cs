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

    private void Update() =>
        _transform.position += _direction * (_speed * Time.deltaTime);
    
    public void ChangeDirection() =>
        _direction = _direction == Vector3.right ? Vector3.forward : Vector3.right;

    public void SetSpeed(float speed) =>
        _speed = speed;
}
