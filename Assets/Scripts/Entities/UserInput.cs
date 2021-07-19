using System;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public event Action OnPress = null;
    
    private void Update()
    {
#if !UNITY_EDITOR
        foreach (var touch in Input.touches)
        {
            if (touch.fingerId == 0 && touch.phase == TouchPhase.Began)
                OnPress?.Invoke();
        }
#else
        if (Input.GetKeyDown(KeyCode.Space))
            OnPress?.Invoke();
#endif
    }
}