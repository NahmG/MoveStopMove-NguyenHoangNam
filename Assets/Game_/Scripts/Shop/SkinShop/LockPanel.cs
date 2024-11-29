using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _money;

    public void SetMoney(int money)
    {
        _money.text = money.ToString();
    }
}
