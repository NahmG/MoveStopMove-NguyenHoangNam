using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasSkinShop : UICanvas
{
    [Header ("Local Prop")]
    [SerializeField] SkinShop shop;

    public void OnClickEvent(ButtonItem btn)
    {
        shop.PressButton(btn);
    }

    public override void Close(float time)
    {
        base.Close(time);

        StageManager.Ins.MainMenu();
    }

}
