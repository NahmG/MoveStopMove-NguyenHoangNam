using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGameplay : UICanvas
{
    [Header("Private props")]
    [SerializeField] TMP_Text enemyCount;

    private void Update()
    {
        enemyCount.text = "Alive: " + CharacterManager.Ins.remainEnemyCount.ToString();
    }

    public void SettingButton()
    {
        UIManager.Ins.CloseUI<CanvasGameplay>();
        StageManager.Ins.Setting();
    }
}
