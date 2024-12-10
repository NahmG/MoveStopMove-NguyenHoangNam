using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : Character
{
    [Header("Player settings")]
    public Rigidbody rb;
    public PlayerMovement movement;

    [HideInInspector] public bool CanMove;

    [SerializeField] CapsuleCollider _collider;

    [SerializeField] CombatText combatTextPrefab;

    [SerializeField] Joystick joystickPrefab;
    [SerializeField] Transform joyStickCanvas;
    public Joystick joystick { get; private set; }

    [SerializeField] GameObject rangeIndicator;

    [field: SerializeField] public CharSkin skin { get; private set; }

    [SerializeField] Color playerColor;

    public StateMachine<Player> StateMachine;

    public PAttackState AttackState;
    public PIdleState IdleState;
    public PMoveState MoveState;

    private void Awake()
    {
        StateMachine = new();

        AttackState = new(StateMachine, this);
        IdleState = new(StateMachine, this);
        MoveState = new(StateMachine, this);
    }

    public override void Update()
    {
        if (IsDead) return;

        base.Update();

        movement.GetInput();

        StateMachine.currentState?.Excecute();
    }

    public override void OnGameStageChanged(GameState state)
    {
        base.OnGameStageChanged(state);

        switch (state)
        {
            case GameState.MainMenu:
                movement.StopMoving();
                animControl.ChangeAnim("Idle");
                break;
            case GameState.Shop:
                animControl.ChangeAnim("Dance");
                break;
            case GameState.Victory:
                animControl.ChangeAnim("Win");
                break;
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        color = playerColor;

        CameraFollow.Ins.SetTarget(this);
        RemoveJoyStick();
        rangeIndicator.SetActive(false);
        _collider.enabled = true;

        skin.SetDefaultTexture();
        skin.GetDefaultSkin();
    }

    public override void OnPlay()
    {
        base.OnPlay();

        AddJoyStick();
        rangeIndicator.SetActive(true);

        CanMove = true;

        StateMachine.Init(IdleState);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        Destroy(gameObject);
    }


    public void OnRevive(Vector3 pos)
    {
        transform.position = pos;

        movement.StopMoving();
        animControl.ResetAnim();

        color = playerColor;
        skin.SetDefaultTexture();

        _collider.enabled = true;
        IsDead = false;
    }

    public override void OnDeath()
    {
        movement.StopMoving();

        base.OnDeath();

        _collider.enabled = false;
        animControl.ChangeAnim("Dead");
        RemoveJoyStick();

        color *= .5f;
        skin.bodyRenderer.material.color = color;

        Invoke(nameof(OnFail), 2f);
    }

    public override void OnVictory()
    {
        if (IsDead) { return; }
        movement.StopMoving();

        rangeIndicator.SetActive(false);
        _collider.enabled = false;
        RemoveJoyStick();

        UIManager.Ins.CloseAll();
        StageManager.Ins.Victory();
    }

    public override void OnFail()
    {
        base.OnFail();

        StageManager.Ins.Fail();
    }

    private void Attack()
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
    }

    protected override void OnLevelUp(float scale)
    {
        base.OnLevelUp(scale);
        CameraFollow.Ins.ChangeOffsetIngame(scale);
        CombatText(attacker.Target);
    }

    public void CombatText(Character target)
    {
        if (target != null)
        {
            Instantiate(combatTextPrefab, transform).OnInit(target.Bonus);
        }
    }

    public void AddJoyStick()
    {
        if (joystick != null)
        {
            Destroy(joystick.gameObject);
        }
        joystick = Instantiate(joystickPrefab, joyStickCanvas);
    }

    public void RemoveJoyStick()
    {
        if (joystick != null)
        {
            Destroy(joystick.gameObject);
        }
        joystick = null;
    }

}
