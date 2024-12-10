using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    [Space]
    [Header("Pool")]
    public List<GameUnit> PoolNoneRoot;

    [Header("Pool")]
    public List<PoolAmount> PoolWithRoot;

    [SerializeField] CharacterManager characterManager;

    public void Awake()
    {
        for (int i = 0; i < PoolNoneRoot.Count; i++)
        {
            MiniPool.Preload(PoolNoneRoot[i], 0, transform);
        }

        for (int i = 0; i < PoolWithRoot.Count; i++)
        {
            MiniPool.Preload(PoolWithRoot[i].prefab, PoolWithRoot[i].amount, PoolWithRoot[i].root);
        }
    }
}

[System.Serializable]
public class PoolAmount
{
    [Header("-- Pool Amount --")]
    public Transform root;
    public GameUnit prefab;
    public int amount;

    public PoolAmount(Transform root, GameUnit prefab, int amount)
    {
        this.root = root;
        this.prefab = prefab;
        this.amount = amount;
    }
}


