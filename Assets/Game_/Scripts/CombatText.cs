using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatText : MonoBehaviour
{
    [SerializeField] TMP_Text bonusText;
    [SerializeField] Animator animator;

    private void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void OnInit(int bonus)
    {
        bonusText.text = "+" + bonus.ToString();
        animator.enabled = true;
        Invoke(nameof(OnDespawn), .75f);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }
}
