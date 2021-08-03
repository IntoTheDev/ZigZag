using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class ColorChanger : ITickable
{
    private readonly IUserInput _userInput = null;
    private readonly Player _player = null;
    private readonly Settings _settings = null;
    private readonly Stopwatch _stopwatch = null;
    private Color _color = default;
    private bool _isEnabled = false;

    public ColorChanger(IUserInput userInput, Player player, Settings settings)
    {
        _userInput = userInput;
        _player = player;
        _settings = settings;
        
        _userInput.OnPress += Enable;
        _player.OnLose += Disable;
        
        _color = _settings.Material.color;
        _stopwatch = new Stopwatch(_settings.ChangeRate);
    }

    public void Tick()
    {
        var material = _settings.Material;
        material.color = Color.Lerp(material.color, _color, _settings.ChangeSpeed);

        if (!_isEnabled || !_stopwatch.IsElapsed)
            return;

        var newColor = _color;
                
        while (newColor == _color)
        {
            var colors = _settings.Colors;
            newColor = colors[Random.Range(0, colors.Length)];
        }

        _color = newColor;
        _stopwatch.Reset();
    }

    private void Enable()
    {
        _stopwatch.Reset();
        _isEnabled = true;
        _userInput.OnPress -= Enable;
    }
    
    private void Disable() =>
        _isEnabled = false;

    [Serializable]
    public class Settings
    {
        [SerializeField] private Material _material = null;
        [SerializeField] private float _changeRate = 10f;
        [SerializeField] private float _changeSpeed = 1f;
        [SerializeField] private Color[] _colors = default;

        public Material Material => _material;
        public float ChangeRate => _changeRate;
        public float ChangeSpeed => _changeSpeed;
        public Color[] Colors => _colors;
    }
}
