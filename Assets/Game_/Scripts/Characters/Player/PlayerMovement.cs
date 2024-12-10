using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    [SerializeField] Player player;

    float verticalMovement;
    float horizontalMovement;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    private Vector3 moveDirection;

    public void GetInput()
    {
        Joystick stick = player.joystick;
        if (stick != null)
        {
            verticalMovement = stick.Horizontal;
            horizontalMovement = stick.Vertical;
        }
    }

    public bool HasInput()
    {
        return verticalMovement != 0 || horizontalMovement != 0;
    }

    public override void Move()
    {
        //if (!player.CanMove) return;

        base.Move();

        moveDirection = new Vector3(verticalMovement, 0, horizontalMovement);
        moveDirection.Normalize();

        player.rb.velocity = moveDirection * moveSpeed;

        if (moveDirection != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(moveDirection, Vector3.up);
            player.Tf.rotation = Quaternion.RotateTowards(player.Tf.rotation, rot, rotateSpeed);

            //Moving = true;
        }
        else
        {
            //Moving = false;
        }
    }

    public override void StopMoving()
    {
        base.StopMoving();

        //player.CanMove = false;
        //Moving = false;

        moveDirection = Vector3.zero;
        player.rb.velocity = Vector3.zero;

        verticalMovement = horizontalMovement = 0;
    }

}
