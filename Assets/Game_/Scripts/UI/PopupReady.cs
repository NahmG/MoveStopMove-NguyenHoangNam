using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupReady : UICanvas
{
    [SerializeField] TMP_Text countText;
    float count;

    private void OnEnable()
    {
        count = 3;
        countText.text = count.ToString();
    }

    void Update()
    {
        count -= Time.deltaTime;
        if(count < 1)
        {
            countText.text = "Play!";
            return;
        }
        countText.text = ((int)count).ToString();
    }
}
