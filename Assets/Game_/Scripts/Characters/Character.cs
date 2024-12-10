using System;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Character : GameUnit
{
    [Header("Character setting")]

    [Header("Model")]
    [HideInInspector] public Weapon currentWeapon;

    [HideInInspector] public Color color;

    protected int level;
    protected int bonus;
    public int Level => level;
    public int Bonus => bonus;

    [Header("Parameter")]
    public float attackRate;

    [SerializeField] float initAttackRange;
    protected float attackRange;
    public float AttackRange => attackRange;

    bool isBoosted;
    public bool IsBoosted => isBoosted;

    [HideInInspector] public bool IsDead { get; protected set; }
    public Animator anim;

    [Header("Scripts")]
    public CharacterAttack attacker;
    public CharacterAnim animControl;

    public Transform Tf;

    public Action<Character> OnDeathAction;
    public Action<Character> OnLevelUpAct;

    public virtual void Update()
    {
   
    }

    public virtual void OnGameStageChanged(GameState state)
    {

    }

    public virtual void OnInit()
    {
        attackRange = initAttackRange;
        level = 0;
        IsDead = false;

        GameManager.Ins._OnStateChanged += OnGameStageChanged;
    }

    public virtual void OnLoad(int level) { }

    public virtual void OnPlay()
    {
        CheckLevel();
    }

    public virtual void OnVictory() { }
    public virtual void OnFail() { }


    public virtual void OnHit()
    {
        OnDeath();
    }

    public virtual void OnDeath()
    {
        IsDead = true;

        attacker.RemoveTarget();

        OnDeathAction?.Invoke(this);
    }

    public virtual void OnDespawn()
    {
        GameManager.Ins._OnStateChanged -= OnGameStageChanged;
    }

    public virtual void LevelUp(Character target)
    {
        level += target.Bonus;
        CheckLevel();
    }

    protected virtual void OnLevelUp(float scale)
    {
        transform.localScale = Vector3.one * scale;
        attackRange = initAttackRange * scale;
    }

    public void CheckLevel()
    {
        if (level < 2)
        {
            OnLevelUp(1);
            bonus = 1;
        }
        else if (level < 6)
        {
            OnLevelUp(1.2f);
            bonus = 2;
        }
        else if (level < 15)
        {
            OnLevelUp(1.5f);
            bonus = 3;
        }
        else if (level < 30)
        {
            OnLevelUp(1.8f);
            bonus = 4;
        }
        else
        {
            OnLevelUp(2.1f);
            bonus = 5;
        }

        OnLevelUpAct?.Invoke(this);
    }

    public void Booster()
    {
        if (!isBoosted)
        {
            isBoosted = true;
            attackRange *= 1.5f;
        }
    }

    public void ResetBooster()
    {
        isBoosted = false;
        CheckLevel();
    }
}
