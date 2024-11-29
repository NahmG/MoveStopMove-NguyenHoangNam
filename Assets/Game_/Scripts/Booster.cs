using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] Ease easeDown;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Character"))
        {
            Character c = col.gameObject.GetComponent<Character>();
            c.Booster();
            OnDespawn();
        }
    }

    public void OnInit()
    {
        transform.DOMoveY(0, 1f).SetEase(easeDown);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }

}
