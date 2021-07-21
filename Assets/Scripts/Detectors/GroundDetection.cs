using System;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField] private LayerMask _layers = default;
    [SerializeField] private float _radius = 0f;
    [SerializeField] private float _distance = 0f;

    private bool _isGrounded = true;
    private readonly Collider[] _result = new Collider[16];
    
    public event Action<bool> OnStateChanged = null;

    private void Update()
    {
        int size = Physics.OverlapSphereNonAlloc(transform.position, _radius, _result, _layers);
        bool isGrounded = size > 0;
        
        if (isGrounded == _isGrounded)
            return;

        _isGrounded = isGrounded;
        OnStateChanged?.Invoke(isGrounded);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * _distance, _radius);
    }
}