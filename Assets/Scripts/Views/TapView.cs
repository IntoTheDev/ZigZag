using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapView : MonoBehaviour
{
    [SerializeField] private TMP_Text _tapText = null;

    private const string START_TEXT = "TAP TO PLAY!";
    private const string RESTART_TEXT = "TAP TO RESTART!";
    
    private void OnEnable()
    {
        _tapText.text = START_TEXT;
        UserInput.OnPress += OnPress;
    }

    private void OnPress()
    {
        _tapText.gameObject.SetActive(false);
        UserInput.OnPress -= OnPress;
        Player.OnLose += OnLose;
    }

    private void OnLose()
    {
        _tapText.text = RESTART_TEXT;
        _tapText.gameObject.SetActive(true);
        UserInput.OnPress += Restart;
        Player.OnLose -= OnLose;
    }

    private void Restart()
    {
        UserInput.OnPress -= Restart;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}