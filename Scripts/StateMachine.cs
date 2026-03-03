using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State CurrentState;

    private void Update()
    {
        CurrentState?.Tick(Time.deltaTime);
    }
    public void SwitchState(State _newState)
    {
        CurrentState?.Exit();
        CurrentState = _newState;
        CurrentState?.Enter();
    }
}