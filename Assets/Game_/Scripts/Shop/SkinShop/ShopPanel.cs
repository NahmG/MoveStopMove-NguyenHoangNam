using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] SkinShop shop;

    [SerializeField] ButtonItem buttonPrefab;
    [SerializeField] Transform tf;

    public ItemType type;
    
    List<ButtonItem> buttons = new();

    public void OnInit()
    {
        Item[] items = DataManager.Ins.itemList[type];
        for (int i = 0; i < items.Length; i++)
        {
            ButtonItem btn = Instantiate(buttonPrefab, tf);
            btn.OnInit(type,i);
            if (DataManager.Ins.itemUnlock[btn.type][i])
            {
                btn.Unlock();
            }
            buttons.Add(btn);   
        }
    }

    public void OnOpen()
    {
        shop.PressButton(buttons.Find(x => x.Id == 0));
    }

}
