using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] Character owner;
 
    public void Fire()
    {
        CharacterAttack attacker = owner.attacker;

        Bullet bullet = MiniPool.Spawn<Bullet>((PoolType)owner.currentWeapon.data.type, shootPoint.position, Quaternion.identity);

        if (bullet != null)
        {
            bullet.Init(owner.AttackRange, attacker.direc, owner);
        }
        else { Debug.LogError("null weapon"); }
    }

    public void ResetAttack()
    {
        owner.animControl.ResetAnim();
    }

}
