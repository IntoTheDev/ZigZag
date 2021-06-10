using System;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _followTo = null;
    [SerializeField] private float _offset = 0f;

    private void OnEnable() =>
        Player.OnLose += OnLose;

    private void OnDisable() =>
        Player.OnLose -= OnLose;

    private void LateUpdate()
    {
        var targetPosition = _followTo.position;
        var myPosition = transform.position;
        targetPosition.x += _offset;
        targetPosition.y = myPosition.y;
        targetPosition.z += _offset;
        
        transform.position = targetPosition;
    }
    
    private void OnLose() =>
        enabled = false;
}
