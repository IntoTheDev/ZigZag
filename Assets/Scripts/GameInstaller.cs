using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private UserInput _userInput = null;
    [SerializeField] private ColorChanger _colorChanger = null;
    [SerializeField] private CameraMover _cameraMover = null;
    [SerializeField] private Player _player = null;
    [SerializeField] private PathGenerator _pathGenerator = null;
    [SerializeField] private TapView _tapView = null;
    [SerializeField] private ScoreView _scoreView = null;
    
    private void Awake()
    {
        _colorChanger.Construct(_userInput, _player);
        _cameraMover.Construct(_userInput, _player);
        _player.Construct(_userInput);
        _pathGenerator.Construct(_userInput);
        _tapView.Construct(_userInput, _player);
        _scoreView.Construct(_player);
    }
}
