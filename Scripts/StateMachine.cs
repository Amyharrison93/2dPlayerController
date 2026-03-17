using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State CurrentState;
    public State PreviousState {get; private set;}

    private void FixedUpdate()
    {
        CurrentState?.Tick(Time.deltaTime);
    }
    public void SwitchState(State _newState)
    {
        CurrentState?.Exit();
        PreviousState = CurrentState;
        CurrentState = _newState;
        CurrentState?.Enter();
    }
}