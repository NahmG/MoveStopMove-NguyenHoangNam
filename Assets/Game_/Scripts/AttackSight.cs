using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackSight : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] CharacterAttack attacker;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] LayerMask characterLayer;

    private void Update()
    {
        if (character.IsDead) return;

        if (attacker.Target == null)
        {
            CheckTarget();
        }
        else if (attacker.Target.IsDead || !attacker.TargetInRange() || Blocked(attacker.Target))
        {
            DeselectEnemy(attacker.Target);
            attacker.SetTarget(null);
        }
    }

    private void CheckTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, character.AttackRange - .15f, characterLayer);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                Character tar = cols[i].GetComponent<Character>();
                if (tar != character && !Blocked(tar))
                {
                    attacker.SetTarget(tar);
                    SelectEnemy(tar);
                    break;
                }
            }
        }
    }


    private bool Blocked(Character tar)
    {
        Vector3 direc = tar.transform.position - character.transform.position;
        direc.y = 0.5f;
        return Physics.Raycast(character.transform.position, direc, direc.magnitude, obstacleLayer);
    }

    private void SelectEnemy(Character enemy)
    {
        if (enemy != null && character.GetComponent<Player>() != null)
        {
            Enemy e = (Enemy)enemy;
            if (e != null)
            {
                e.OnSelected();
            }
        }
    }

    private void DeselectEnemy(Character enemy)
    {
        if (enemy != null && character.GetComponent<Player>() != null)
        {
            Enemy e = (Enemy)enemy;
            if (e != null)
            {
                e.DeSelected();
            }
        }
    }
}
