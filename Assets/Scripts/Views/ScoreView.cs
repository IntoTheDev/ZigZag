using TMPro;
using UnityEngine;
using Zenject;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreCounter = null;

    [Inject]
    private void Construct(Player player) =>
        player.OnScoreChanged += OnScoreChanged;

    private void OnScoreChanged(int totalAmount) =>
        _scoreCounter.text = totalAmount.ToString();
}
