using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [Header("Enemy settings")]
    public EnemyMovement movement;
    [SerializeField] Renderer rend;

    [SerializeField] CapsuleCollider _collider;
    [SerializeField] GameObject TargetRing;
    public NavMeshAgent agent;

    [SerializeField] CharSkin skin;

    public event Action<Enemy> OnDeadEvent;

    public StateMachine<Enemy> StateMachine;
    public EIdleState IdleState;
    public EMoveState MoveState;
    public EAttackState AttackState;

    private void Awake()
    {
        StateMachine = new();

        IdleState = new EIdleState(StateMachine, this);
        MoveState = new EMoveState(StateMachine, this);
        AttackState = new EAttackState(StateMachine, this);
    }

    public override void Update()
    {
        if (IsDead) return;

        base.Update();

        StateMachine.currentState?.Excecute();
    }

    public override void OnGameStageChanged(GameState state)
    {
        base.OnGameStageChanged(state);

        switch (state)
        {
            case GameState.MainMenu:
                StateMachine.ChangeState(null);
                break;
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

        StateMachine.ChangeState(null);
    }

    public override void OnPlay()
    {
        base.OnPlay();

        StateMachine.Init(IdleState);
    }

    public override void OnDeath()
    {
        base.OnDeath();

        movement.StopMoving();
        animControl.ChangeAnim("Dead");
        _collider.enabled = false;

        color *= .5f;
        rend.material.color = color;

        Invoke(nameof(OnDespawn), 2f);
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


    public void InitLevel(int level)
    {
        this.level = level;
    }

    public void Selected()
    {
        TargetRing.SetActive(true);
    }

    public void DeSelected()
    {
        TargetRing.SetActive(false);
    }
}
