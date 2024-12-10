using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EIdleState : State<Enemy>
{
    private Enemy _enemy;

    protected float idleTime;
    protected bool isIdleTimeOver;

    public EIdleState(StateMachine<Enemy> stateMachine, Enemy enemy) : base(stateMachine, enemy)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.movement.StopMoving();
        _enemy.animControl.ChangeAnim(Constant.ANIM_IDLE);

        SetRandomIdleTime();
    }

    public override void Excecute()
    {
        base.Excecute();

        if (Time.time < startTime + idleTime)
        {
            if (_enemy.attacker.HasTarget())
            {
                stateMachine.ChangeState(_enemy.AttackState);
            }
        }
        else
        {
            stateMachine.ChangeState(_enemy.MoveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(1, 1.5f);
    }
}
