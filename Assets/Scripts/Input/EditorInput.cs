using System;
using UnityEngine;
using Zenject;

public class EditorInput : IUserInput, ITickable
{
    public event Action OnPress = null;

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnPress?.Invoke();
    }
}