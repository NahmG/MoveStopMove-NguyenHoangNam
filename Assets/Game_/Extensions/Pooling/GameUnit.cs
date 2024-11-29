using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnit : MonoBehaviour
{
    private Transform tf;
    public Transform TF
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }

    public PoolType poolType;

    public void OnDespawn(float delay)
    {
        Invoke(nameof(OnDespawn), delay);
    }

    private void OnDespawn()
    {
        MiniPool.Despawn(this);
    }
}

public enum PoolType
{
    None = 0,

    Enemy = 1,
    TargetIndicator = 2,

    Hammer = 3,
    Knife = 4,
    Lollipop = 5,
}
