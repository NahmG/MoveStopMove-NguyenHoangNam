using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkinShop : MonoBehaviour
{
    ButtonItem currentButton;
    [SerializeField] LockPanel lockPanel;
    [SerializeField] SelectPanel selectPanel;
    [SerializeField] TMP_Text discription;

    public void PressButton(ButtonItem btn)
    {
        if (btn != currentButton)
        {
            if (currentButton != null)
            {
                currentButton.DeselectButton();
            }
            currentButton = btn;

            if(currentButton != null)
            {
                currentButton.SelectButton();

                CheckItemLockState(currentButton);

                TryItem(currentButton);
            }
            
        }
    }

    private void CheckItemLockState(ButtonItem btn)
    {
        PlayerData playerData = DataManager.Ins.playerData;

        Item item = DataManager.Ins.itemList[btn.type][btn.Id];

        discription.text = item.data.discription;

        if (DataManager.Ins.itemUnlock[btn.type][btn.Id])
        {
            UnlockItem(btn);
            btn.Unlock();
        }
        else
        {
            LockItem(item);
        }
    }

    private void UnlockItem(ButtonItem btn)
    {
        selectPanel.gameObject.SetActive(true);
        lockPanel.gameObject.SetActive(false);
        if (btn.Id == DataManager.Ins.itemIdList[btn.type])
        {
            selectPanel.TurnOnUnEquipButton();
        }
        else
        {
            selectPanel.TurnOnEquipButton();
        }
    }

    private void LockItem(Item item)
    {
        lockPanel.gameObject.SetActive(true);
        lockPanel.SetMoney(item.data.price);

        selectPanel.gameObject.SetActive(false);
    }

    private void TryItem(ButtonItem btn)
    {
        CharacterManager.Ins.Player.skin.AddItem(btn.type, btn.Id);
    }

    public void BuyButton()
    {
        Item item = DataManager.Ins.itemList[currentButton.type][currentButton.Id];

        if (DataManager.Ins.playerData.coin >= item.data.price)
        {
            DataManager.Ins.playerData.coin -= item.data.price;

            DataManager.Ins.itemUnlock[currentButton.type][currentButton.Id] = true;    

            DataManager.Ins.SaveData();

            currentButton.Unlock();

            lockPanel.gameObject.SetActive(false);
            selectPanel.gameObject.SetActive(true);
        }
    }

    public void EquipButton()
    {
        DataManager.Ins.itemIdList[currentButton.type] = currentButton.Id;

        if (currentButton.type != ItemType.Suit)
        {
            DataManager.Ins.itemIdList[ItemType.Suit] = -1;
        }

        DataManager.Ins.SaveData();

        CharacterManager.Ins.Player.skin.AddItem(currentButton.type, currentButton.Id);
        selectPanel.TurnOnUnEquipButton();
    }

    public void UnEquipButton()
    {
        DataManager.Ins.itemIdList[currentButton.type] = -1;
        DataManager.Ins.SaveData();

        CharacterManager.Ins.Player.skin.RemoveItem(currentButton.type); 
        selectPanel.TurnOnEquipButton();
    }
}
