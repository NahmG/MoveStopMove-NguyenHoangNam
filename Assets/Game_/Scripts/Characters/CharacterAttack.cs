using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] Character owner;

    [HideInInspector] public Vector3 direc;

    Character target;
    public Character Target => target;

    public void Attack()
    {
        if (target != null)
        {
            direc = target.transform.position - transform.position;
            direc.y = 0;
            direc.Normalize();
            Quaternion rotation = Quaternion.LookRotation(direc);
            transform.rotation = rotation;

            if (owner.IsBoosted)
            {
                owner.animControl.ChangeAnim("Ulti");
            }
            else
            {
                owner.animControl.ChangeAnim("Attack");
            }
        }
    }

    public void SetTarget(Character tar)
    {
        if (!target)
        {
            target = tar;
            if (target is Enemy enemy && owner is Player)
            {
                enemy.Selected();
            }
        }
    }

    public void RemoveTarget()
    {
        if (target != null && target is Enemy enemy)
        {
            enemy.DeSelected();
        }
        target = null;
    }

    public bool HasTarget()
    {
        return target != null && TargetInRange();
    }

    private bool TargetInRange()
    {
        return target != null && Vector3.Distance(transform.position, target.transform.position) < owner.AttackRange;
    }

}
