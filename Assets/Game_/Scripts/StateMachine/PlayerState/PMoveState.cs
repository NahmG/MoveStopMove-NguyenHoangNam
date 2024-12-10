using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMoveState : State<Player>
{
    private Player player;

    public PMoveState(StateMachine<Player> stateMachine, Player entity) : base(stateMachine, entity)
    {
        player = entity;
    }

    public override void Enter()
    {
        base.Enter();

        player.animControl.ChangeAnim(Constant.ANIM_RUN);
    }

    public override void Excecute()
    {
        base.Excecute();

        player.movement.Move();

        if (!player.movement.HasInput())
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
