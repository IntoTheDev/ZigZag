using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreCounter = null;

    public void Construct(Player player) =>
        player.OnScoreChanged += OnScoreChanged;

    private void OnScoreChanged(int totalAmount) =>
        _scoreCounter.text = totalAmount.ToString();
}
