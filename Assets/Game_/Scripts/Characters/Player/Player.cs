using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : Character
{
    [Header("Player settings")]
    public Rigidbody rb;

    [HideInInspector] public bool CanMove;

    [SerializeField] CapsuleCollider _collider;

    private float nextAttack = 0f;

    [SerializeField] CombatText combatTextPrefab;

    [SerializeField] Joystick joystickPrefab;
    [SerializeField] Transform joyStickCanvas;
    public Joystick joystick { get; private set; }

    [SerializeField] GameObject rangeIndicator;

    [field: SerializeField] public CharSkin skin { get; private set; }

    [SerializeField] Color playerColor;

    public override void Update()
    {
        if (IsDead) return;

        base.Update();

        movement.Move();

        if (movement.Moving)
        {
            animControl.ChangeAnim("Run");
            nextAttack = Time.time;
            return;
        }

        else if (attacker.Target != null && attacker.TargetInRange())
        {
            if (Time.time >= nextAttack)
            {
                Attack();
                nextAttack = Time.time + attackRate;
            }
        }

        //else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        else
        {
            animControl.ChangeAnim("Idle");
        }

        if (GameManager.IsState(GameState.Shop))
        {
            animControl.ChangeAnim("Dance");
        }

        if (GameManager.IsState(GameState.Victory))
        {
            animControl.ChangeAnim("Win");
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
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        UIManager.Ins.CloseAll();
        StageManager.Ins.Fail();
    }

    public void Revive(Vector3 pos)
    {
        transform.position = pos;

        movement.StopMoving();
        animControl.ResetAnim();

        color = playerColor;
        skin.SetDefaultTexture();

        _collider.enabled = true;
        IsDead = false;
    }

    public override void Die()
    {
        movement.StopMoving();

        base.Die();

        _collider.enabled = false;
        animControl.ChangeAnim("Dead");
        RemoveJoyStick();

        color *= .5f;
        skin.bodyRenderer.material.color = color;

        Invoke(nameof(OnDespawn), 2f);
    }

    public void Victory()
    {
        if (IsDead) { return; }
        movement.StopMoving();

        rangeIndicator.SetActive(false);
        _collider.enabled = false;
        RemoveJoyStick();

        UIManager.Ins.CloseAll();
        StageManager.Ins.Victory();
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
    }

    public void CombatText(Character target)
    {
        Instantiate(combatTextPrefab, transform).OnInit(target.Bonus);
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
