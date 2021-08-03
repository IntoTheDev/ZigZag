using UnityEngine;
using Zenject;

public class CameraMover : MonoBehaviour
{
    private GameConfig _config = null;
    private Transform _transform = null;
    private bool _canMove = false;
    private IUserInput _userInput = null;
    private Player _player = null;
    
    [Inject]
    private void Construct(IUserInput userInput, Player player, GameConfig config)
    {
        _userInput = userInput;
        _player = player;
        _config = config;
        
        _userInput.OnPress += EnableMovement;
        _player.OnLose += DisableMovement;
    }
    
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

    private void DisableMovement() =>
        enabled = false;
    
    private void EnableMovement()
    {
        _canMove = true;
        _userInput.OnPress -= EnableMovement;
    }
}
