using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    [SerializeField] TMP_Text coinText;

    public override void Open()
    {
        base.Open();

        int coinEarn = CharacterManager.Ins.Player.Level;
        coinText.text = coinEarn.ToString();

        DataManager.Ins.playerData.coin += coinEarn;
        DataManager.Ins.SaveData();

    }

    public void BackToMainMenu()
    {
        UIManager.Ins.CloseUI<CanvasVictory>();
        StageManager.Ins.OnReset();
        StageManager.Ins.DelayInit(1f);
    }
}
