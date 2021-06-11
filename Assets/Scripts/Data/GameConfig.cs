using UnityEngine;

[CreateAssetMenu(menuName = "GameConfig", fileName = "GameConfig", order = 0)]
public class GameConfig : ScriptableObject
{
    [SerializeField] private float _speed = 4f;

    public float Speed => _speed;
}
