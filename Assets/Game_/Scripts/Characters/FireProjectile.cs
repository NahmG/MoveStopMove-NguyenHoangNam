using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] Character c;
 
    public void Fire()
    {
        CharacterAttack attacker = c.attacker;

        Bullet bullet = MiniPool.Spawn<Bullet>((PoolType)c.currentWeapon.data.type, shootPoint.position, Quaternion.identity);

        if (bullet != null)
        {
            bullet.Init(c.AttackRange, attacker.direc, c);
        }
        else { Debug.LogError("null weapon"); }
    }

    public void EndBooster()
    {
        c.ResetBooster();
    }

}
