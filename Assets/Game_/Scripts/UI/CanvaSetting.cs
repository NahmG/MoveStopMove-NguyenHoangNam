using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaSetting : UICanvas
{
    [SerializeField] GameObject panel;

    public override void Open()
    {
        base.Open();
        panel.transform.DOScale(1, .3f).SetEase(Ease.OutBack);
    }

    public void HomeButton()
    {
        panel.transform.localScale = Vector3.zero;

        UIManager.Ins.CloseUI<CanvaSetting>();
        StageManager.Ins.OnReset();
        StageManager.Ins.DelayInit(1f);
    }

    public void ContinueButton()
    {
        panel.transform.DOScale(0, .3f).SetEase(Ease.InBack);

        UIManager.Ins.CloseUI<CanvaSetting>(.3f);
        StageManager.Ins.Continue();
    }
}
