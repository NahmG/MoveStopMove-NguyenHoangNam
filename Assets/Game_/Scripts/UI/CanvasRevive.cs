using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasRevive : UICanvas
{
    [Header("Canvas Setting")]
    [SerializeField] TMP_Text countDownText;
    [SerializeField] Transform countDownImage;

    [SerializeField] CanvasGroup canvasGroup;

    [SerializeField] float delayTime;

    Sequence s;
    int index;
    float timer;

    public override void Open()
    {
        base.Open();

        s = DOTween.Sequence();
        s.Append(DOVirtual.Float(0, 1, delayTime, x => { canvasGroup.alpha = x; }));

        index = 5;
        timer = Time.time;
    }

    public override void Close(float time)
    {
        base.Close(time);

        canvasGroup.alpha = 0;
    }

    private void Update()
    {
        countDownImage.Rotate(0, 0, -360f * Time.deltaTime);

        if (Time.time >= timer)
        {
            timer = Time.time + 1;

            index--;
            if (index < 0)
            {
                UIManager.Ins.CloseUI<CanvasRevive>();
                UIManager.Ins.OpenUI<CanvasFail>();
            }
        }
        countDownText.text = index.ToString();
    }

    public void ReviveButton()
    {
        UIManager.Ins.CloseUI<CanvasRevive>();
        StageManager.Ins.Revive();
    }

    public void CloseButton()
    {
        UIManager.Ins.CloseUI<CanvasRevive>();
        UIManager.Ins.OpenUI<CanvasFail>();
    }
}
