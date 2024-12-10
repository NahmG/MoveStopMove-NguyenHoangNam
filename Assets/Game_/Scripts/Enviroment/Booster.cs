using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Booster : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Constant.CHARACTER_TAG))
        {
            Character c = Cache.GenCharacter(col);
            c.Booster();
            OnDespawn();
        }
    }

    public void OnInit()
    {
        transform.DOMoveY(0, 1f).SetEase(Ease.InCubic);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
        CharacterManager.Ins.AddBooster();
    }

}
