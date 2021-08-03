using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class TapView : MonoBehaviour
{
    [SerializeField] private TMP_Text _tapText = null;

    private IUserInput _userInput = null;
    private Player _player = null;
    
    private const string START_TEXT = "TAP TO PLAY!";
    private const string RESTART_TEXT = "TAP TO RESTART!";
    
    [Inject]
    private void Construct(IUserInput userInput, Player player)
    {
        _userInput = userInput;
        _player = player;
        
        _userInput.OnPress += OnPress;
    }
    
    private void OnEnable() =>
        _tapText.text = START_TEXT;

    private void OnPress()
    {
        _tapText.gameObject.SetActive(false);
        _userInput.OnPress -= OnPress;
        _player.OnLose += OnLose;
    }

    private void OnLose()
    {
        _tapText.text = RESTART_TEXT;
        _tapText.gameObject.SetActive(true);
        _userInput.OnPress += Restart;
    }

    private void Restart() =>
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}