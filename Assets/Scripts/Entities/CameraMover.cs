using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameConfig _config = null;

    private Transform _transform = null;
    private bool _canMove = false;

    private void Awake() =>
        _transform = transform;

    private void OnEnable()
    {
        Player.OnLose += OnLose;
        UserInput.OnPress += OnPress;
    }

    private void OnDisable() =>
        Player.OnLose -= OnLose;

    private void Update()
    {
        if (!_canMove)
            return;
        
        var moveBy = _transform.forward * (_config.Speed * Time.deltaTime);
        moveBy.y = 0f;

        _transform.position += moveBy;
    }
    
    private void OnLose() =>
        enabled = false;
    
    private void OnPress()
    {
        _canMove = true;
        UserInput.OnPress -= OnPress;
    }
}
