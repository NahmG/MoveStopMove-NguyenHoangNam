using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackSight : MonoBehaviour
{
    [SerializeField] Character owner;
    [SerializeField] CharacterAttack attacker;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] LayerMask characterLayer;
    [SerializeField] SphereCollider _col;

    List<Character> targets = new();

    private void OnEnable()
    {
        owner.OnLevelUpAct += OnLevelUp;
    }

    private void OnDisable()
    {
        owner.OnLevelUpAct -= OnLevelUp;
    }

    public void OnLevelUp(Character cha)
    {
        _col.radius = cha.AttackRange;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Constant.CHARACTER_TAG))
        {
            Character tar = Cache.GenCharacter(col);
            AddTarget(tar);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(Constant.CHARACTER_TAG))
        {
            Character tar = Cache.GenCharacter(col);
            RemoveTarget(tar);
        }
    }

    public void AddTarget(Character tar)
    {
        if (!targets.Contains(tar)) targets.Add(tar);
        tar.OnDeathAction += RemoveTarget;

        UpdateCharacterTarget();
    }

    public void RemoveTarget(Character tar)
    {
        if (targets.Contains(tar)) targets.Remove(tar);

        if (attacker.Target && attacker.Target.Equals(tar))
        {
            attacker.RemoveTarget();
        }

        tar.OnDeathAction -= RemoveTarget;

        UpdateCharacterTarget();
    }

    public void UpdateCharacterTarget()
    {
        if (targets.Count > 0)
        {
            attacker.SetTarget(targets[0]);
        }
    }

    private bool Blocked(Character tar)
    {
        Vector3 direc = tar.Tf.position - owner.Tf.position;
        direc.y = 0.5f;
        return Physics.Raycast(owner.Tf.position, direc, direc.magnitude, obstacleLayer);
    }
}
