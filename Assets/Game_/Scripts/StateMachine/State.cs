using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T>
{
    protected T entity;
    protected StateMachine<T> stateMachine;
    public float startTime;

    public State(StateMachine<T> stateMachine, T entity)
    {
        this.stateMachine = stateMachine;
        this.entity = entity;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
    }

    public virtual void Excecute()
    {

    }

    public virtual void Exit()
    {

    }
}
