using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAttackState : State<Player>
{
    private Player player;
    private float nextAttack;
    public PAttackState(StateMachine<Player> stateMachine, Player entity) : base(stateMachine, entity)
    {
        player = entity;
    }

    public override void Enter()
    {
        base.Enter();

        player.movement.StopMoving();
    }

    public override void Excecute()
    {
        base.Excecute();

        if (player.movement.HasInput())
        {
            nextAttack = Time.time;
            stateMachine.ChangeState(player.MoveState);
        }

        if (Time.time > nextAttack)
        {
            player.attacker.Attack();
            nextAttack = Time.time + player.attackRate;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
