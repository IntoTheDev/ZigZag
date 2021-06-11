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
    }

    private void OnEnable()
    {
        UserInput.OnPress += OnPress;
        Player.OnLose += OnLose;
    }

    private void Update() =>
        _material.color = Color.Lerp(_material.color, _color, _changeSpeed);

    private void OnPress()
    {
        StartCoroutine(ChangeColor());
        UserInput.OnPress -= OnPress;
    }
    
    private void OnLose()
    {
        Player.OnLose -= OnLose;
        StopCoroutine(ChangeColor());
        enabled = false;
    }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            yield return _waitForSeconds;
            
            var newColor = _color;
                
            while (newColor == _color)
                newColor = _colors[Random.Range(0, _colors.Length)];

            _color = newColor;
        }
    }
}
