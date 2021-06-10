using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Material _material = null;
    [SerializeField] private float _changeRate = 10f;
    [SerializeField] private float _changeSpeed = 1f;
    [SerializeField] private Color[] _colors = default;
    
    private WaitForSeconds _waitForSeconds = null;
    private Color _color = default;
    
    private void Awake()
    {
        _color = _material.color;
        _waitForSeconds = new WaitForSeconds(_changeRate);
        Player.OnTap += OnTap;
    }

    private void Update() =>
        _material.color = Color.Lerp(_material.color, _color, _changeSpeed);

    private void OnTap()
    {
        StartCoroutine(ChangeColor());
        Player.OnTap -= OnTap;
    }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            yield return _waitForSeconds;
            Color newColor = _color;
                
            while (newColor == _color)
                newColor = _colors[Random.Range(0, _colors.Length)];

            _color = newColor;
        }
    }
}
