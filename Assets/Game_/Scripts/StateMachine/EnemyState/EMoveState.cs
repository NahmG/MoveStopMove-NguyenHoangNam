using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMoveState : State<Enemy>
{
    private Enemy _enemy;
    protected float moveTime;

    public EMoveState(StateMachine<Enemy> stateMachine, Enemy entity) : base(stateMachine, entity)
    {
        _enemy = entity;
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.animControl.ChangeAnim(Constant.ANIM_RUN);
        SetRandomMoveTime();
    }

    public override void Excecute()
    {
        base.Excecute();

        _enemy.movement.Move();

        if (_enemy.attacker.HasTarget())
        {
            if (startTime > moveTime)
            {
                stateMachine.ChangeState(_enemy.AttackState);
            }
        }
        else if (_enemy.movement.IsReachDestination())
        {
            stateMachine.ChangeState(_enemy.IdleState);
        }
    }

    public override void Exit()
    {

    }

    private void SetRandomMoveTime()
    {
        moveTime = Random.Range(.5f, 2);
    }
}
