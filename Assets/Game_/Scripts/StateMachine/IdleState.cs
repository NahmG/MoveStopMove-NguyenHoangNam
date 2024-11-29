using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : IState
{
    float timer;
    float randomTime;

    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        randomTime = Random.Range(1f, 1.5f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (timer < randomTime)
        {
            if (enemy.attacker.Target != null && enemy.attacker.TargetInRange())
            {
                enemy.ChangeState(new AttackState());
            }
        }
        else
        {
            enemy.ChangeState(new MoveState());
        }
    }

    public void OnExit(Enemy enemy)
    {
    }
}
