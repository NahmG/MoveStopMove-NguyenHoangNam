using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasCoin : UICanvas
{
    [SerializeField] TMP_Text coinText;
    [SerializeField] GameObject coin;


    public override void Open()
    {
        base.Open();
        coin.transform.DOLocalMoveX(425, 0f); 

    }

    public void OnClose()
    {
        coin.transform.DOLocalMoveX(transform.position.x + 500, .25f);
    }

    void Update()
    {
        coinText.text = DataManager.Ins.playerData.coin.ToString();    
    }
    
    
}
