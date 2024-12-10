using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    public State<T> currentState {  get; private set; }

    public void Init(State<T> initState)
    {
        currentState = initState;
        currentState?.Enter();
    }

    public void ChangeState(State<T> newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}
