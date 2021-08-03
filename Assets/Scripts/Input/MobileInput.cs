using System;
using UnityEngine;
using Zenject;

public class MobileInput : IUserInput, ITickable
{
    public event Action OnPress = null;

    public void Tick()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.fingerId == 0 && touch.phase == TouchPhase.Began)
                OnPress?.Invoke();
        }
    }
}