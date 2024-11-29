using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    float timer;
    float randomTime;

    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        randomTime = Random.Range(.5f, 2f);
    }

    public void OnExecute(Enemy enemy)
    {
        
        enemy.movement.Move();
        
        if (enemy.attacker.Target != null  && enemy.attacker.TargetInRange())
        {
            timer += Time.deltaTime;
            if(timer > randomTime)
            {
                enemy.ChangeState(new AttackState());
            }
        }
        else if(enemy.agent.remainingDistance > 0 && enemy.agent.remainingDistance < .1f)
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
