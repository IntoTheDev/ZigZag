using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    
    private Vector3 _direction = default;

    private void Awake() =>
        _direction = Vector3.right;

    public void ChangeDirection() =>
        _direction = _direction == Vector3.right ? Vector3.forward : Vector3.right;

    private void Update() =>
        transform.position += _direction * (_speed * Time.deltaTime);
}
