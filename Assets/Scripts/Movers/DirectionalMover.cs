using UnityEngine;

public class DirectionalMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Vector3 _direction = default;

    private void Update() =>
        transform.position += _direction * (_speed * Time.deltaTime);
}
