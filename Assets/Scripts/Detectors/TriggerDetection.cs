using System;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    [SerializeField] private string _tag = string.Empty;

    public event Action<Collider> OnEnter = null;
    public event Action<Collider> OnExit = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag))
            OnEnter?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tag))
            OnExit?.Invoke(other);
    }
}