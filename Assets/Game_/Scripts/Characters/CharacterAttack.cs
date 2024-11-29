using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] Character character;

    [HideInInspector] public Vector3 direc;

    [SerializeField] Character target;
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
        }
    }

    public void SetTarget(Character target)
    {
        this.target = target;
    }

    public bool TargetInRange()
    {
        if (target != null && Vector3.Distance(transform.position, target.transform.position) < character.AttackRange)
        {
            return true;
        }
        else { return false; }
    }

}
