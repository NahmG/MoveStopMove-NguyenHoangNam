using UnityEngine;

public class Character : GameUnit
{
    [Header ("Character setting")]

    [Header ("Model")]
    public Weapon currentWeapon;

    [HideInInspector] public Color color;

    protected int level;
    protected int bonus;
    public int Level => level;
    public int Bonus => bonus;

    [Header ("Parameter")]
    [SerializeField] protected float attackRate;

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
    public CharacterMovement movement;

    public virtual void Update()
    {
        CheckLevel();

        if (GameManager.IsState(GameState.MainMenu))
        {
            movement.StopMoving();
        }

        if (IsBoosted) { attackRange *= 1.5f; }
    }

    public virtual void OnInit()
    {
        attackRange = initAttackRange;
        level = 0;
        IsDead = false;

        movement.StopMoving();
    }

    public virtual void OnDespawn()
    {

    }

    public virtual void OnPlay()
    {

    }

    public void LevelUp(Character target)
    {
        level += target.Bonus;
    }

    public virtual void Die()
    {
        IsDead = true;
    }

    protected virtual void OnLevelUp(float scale)
    {
        transform.localScale = Vector3.one * scale;
        attackRange = initAttackRange * scale;
    }
    
    private void CheckLevel()
    {
        if(level < 2)
        {
            OnLevelUp(1);
            bonus = 1;
        }
        else if(level < 6)
        {
            OnLevelUp(1.2f);
            bonus = 2;
        }
        else if(level < 15)
        {
            OnLevelUp(1.5f);
            bonus = 3;
        }
        else if(level < 30)
        {
            OnLevelUp(1.8f);
            bonus = 4;
        }
        else
        {
            OnLevelUp(2.1f);
            bonus = 5;
        }
    }

    public void Booster()
    {
        isBoosted = true;
    }

    public void ResetBooster()
    {
        isBoosted = false;
    }

}
