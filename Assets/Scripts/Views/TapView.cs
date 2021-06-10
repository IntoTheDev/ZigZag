using UnityEngine;

public class TapView : MonoBehaviour
{
    [SerializeField] private GameObject _tapToPlay = null;
    
    private void OnEnable() =>
        Player.OnTap += OnTap;

    private void OnDisable() =>
        Player.OnTap -= OnTap;

    private void OnTap() =>
        _tapToPlay.SetActive(false);
}