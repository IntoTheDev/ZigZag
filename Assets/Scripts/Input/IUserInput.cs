using System;
using Zenject;

public interface IUserInput
{
    event Action OnPress;
}