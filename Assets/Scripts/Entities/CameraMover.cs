using UnityEngine;
using Zenject;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameConfig _config = null;

    private Transform _transform = null;
    private bool _canMove = false;
    private UserInput _userInput = null;
    private Player _player = null;
    
    private void Awake() =>
        _transform = transform;

    private void Update()
    {
        if (!_canMove)
            return;
        
        var moveBy = _transform.forward * (_config.Speed * Time.deltaTime);
        moveBy.y = 0f;

        _transform.position += moveBy;
    }
    
    [Inject]
    private void Construct(UserInput userInput, Player player)
    {
        _userInput = userInput;
        _player = player;
        
        _userInput.OnPress += OnPress;
        _player.OnLose += OnLose;
    }
    
    private void OnLose() =>
        enabled = false;
    
    private void OnPress()
    {
        _canMove = true;
        _userInput.OnPress -= OnPress;
    }
}
