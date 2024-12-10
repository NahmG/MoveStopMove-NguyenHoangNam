using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIdleState : State<Player>
{
    private Player player;

    public PIdleState(StateMachine<Player> stateMachine, Player entity) : base(stateMachine, entity)
    {
        player = entity;
    }

    public override void Enter()
    {
        base.Enter();

        player.animControl.ChangeAnim(Constant.ANIM_IDLE);

        player.movement.StopMoving();

    }

    public override void Excecute()
    {
        base.Excecute();

        if (player.movement.HasInput())
        {
            stateMachine.ChangeState(player.MoveState);
        }

        else if (player.attacker.HasTarget())
        {
            stateMachine.ChangeState(player.AttackState);   
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
