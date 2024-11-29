using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasWeapon : UICanvas
{
    public override void Close(float delayTime)
    {
        base.Close(delayTime);
        StageManager.Ins.MainMenu();
    }
}
