using ToolBox.Pools;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    [SerializeField] private int _score = 0;
    
    public int Take()
    {
        gameObject.Release();
        return _score;
    }
}
