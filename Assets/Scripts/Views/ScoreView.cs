using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreCounter = null;

    private void OnEnable() =>
        Player.OnScoreChanged += OnScoreChanged;

    private void OnDisable() =>
        Player.OnScoreChanged -= OnScoreChanged;

    private void OnScoreChanged(int totalAmount) =>
        _scoreCounter.text = totalAmount.ToString();
}
