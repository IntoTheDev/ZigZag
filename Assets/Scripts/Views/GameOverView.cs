using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private GameObject _root = null;
    [SerializeField] private Button _restartButton = null;
    
    private void OnEnable()
    {
        Player.OnLose += OnLose;
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        Player.OnLose -= OnLose;
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
    }

    private void OnLose() =>
        _root.SetActive(true);
    
    private void OnRestartButtonClick() =>
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
