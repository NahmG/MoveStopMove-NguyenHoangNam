using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [Header("Enemy settings")]
    [SerializeField] Renderer rend;

    [SerializeField] CapsuleCollider _collider;
    [SerializeField] GameObject TargetRing;
    public NavMeshAgent agent;
    float nextAttack = 0f;

    [SerializeField] CharSkin skin;

    public IState currentState { get; private set; }

    public static event Action<Enemy> OnDeadEvent;

    public override void Update()
    {
        if (IsDead) return;

        base.Update();

        currentState?.OnExecute(this);

        if (movement.Moving && currentState is MoveState)
        {
            animControl.ChangeAnim("Run");
        }
        else if (currentState is not AttackState)
        {
            animControl.ChangeAnim("Idle");
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        rend.material.color = color;

        GetSkin();

        DeSelected();
        _collider.enabled = true;
        ChangeState(null);
    }

    public override void OnPlay()
    {
        base.OnPlay();

        ChangeState(new IdleState());
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        OnDeadEvent?.Invoke(this);
    }

    public void GetSkin()
    {
        DataManager data = DataManager.Ins;

        int weaponId = UnityEngine.Random.Range(0, data.weapons.Count);
        skin.AddWeapon(data.weapons[weaponId]);

        Dictionary<ItemType, int> itemId = new()
        {
            {ItemType.Hat, UnityEngine.Random.Range(-1, data.itemList[ItemType.Hat].Length)},
            {ItemType.Pant, UnityEngine.Random.Range(-1, data.itemList[ItemType.Pant].Length)},
            {ItemType.Shield, UnityEngine.Random.Range(-1, data.itemList[ItemType.Shield].Length)},
            {ItemType.Suit, UnityEngine.Random.Range(-1, data.itemList[ItemType.Suit].Length)},
        };

        foreach (ItemType type in itemId.Keys)
        {
            if (itemId[type] >= 0)
            {
                skin.AddItem(type, itemId[type]);
            }
            else
            {
                skin.RemoveItem(type);
            }
        }
    }

    public void Attack()
    {
        if (Time.time >= nextAttack)
        {
            animControl.ResetAnim();
            if (IsBoosted)
            {
                animControl.ChangeAnim("Ulti");
            }
            else
            {
                animControl.ChangeAnim("Attack");
            }
            attacker.Attack();
            nextAttack = Time.time + attackRate;
        }
    }

    public override void Die()
    {
        base.Die();

        movement.StopMoving();
        animControl.ChangeAnim("Dead");
        _collider.enabled = false;

        color *= .5f;
        rend.material.color = color;

        Invoke(nameof(OnDespawn), 2f);
    }

    public void InitLevel(int level)
    {
        this.level = level;
    }

    public void OnSelected()
    {
        TargetRing.SetActive(true);
    }

    public void DeSelected()
    {
        TargetRing.SetActive(false);
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
}
