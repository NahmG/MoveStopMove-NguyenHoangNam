using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAttackState : State<Enemy>
{
    private Enemy _enemy;
    protected float nextAttack;

    public EAttackState(StateMachine<Enemy> stateMachine, Enemy entity) : base(stateMachine, entity)
    {
        _enemy = entity;
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.movement.StopMoving();

    }

    public override void Excecute()
    {
        base.Excecute();

        if (Time.time > nextAttack)
        {
            _enemy.attacker.Attack();
            nextAttack = Time.time + _enemy.attackRate;
        }

        if (!_enemy.attacker.HasTarget())
        {
            stateMachine.ChangeState(_enemy.MoveState);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}
