using System;
using UnityEngine;
using Zenject;

public class UserInput : ITickable
{
    public event Action OnPress = null;

    public void Tick()
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