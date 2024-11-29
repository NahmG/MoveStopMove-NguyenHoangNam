using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonItem : MonoBehaviour
{
    public ItemType type;
    public int Id;
    [SerializeField] GameObject selectionFrame;
    [SerializeField] GameObject lockIcon;
    [SerializeField] Image image;
    [SerializeField] Button button;
 
    public void OnInit(ItemType type, int id)
    {
        Id = id;
        this.type = type;

        Item item = DataManager.Ins.itemList[this.type][Id];
        image.sprite = item.data.sprite;

        button.onClick.AddListener(OnClickEvent);
    }

    public void SelectButton()
    {
        selectionFrame.SetActive(true);
    }

    public void DeselectButton()
    {
        selectionFrame.SetActive(false);
    }

    public void Unlock()
    {
        lockIcon.SetActive(false);
    }

    public void OnClickEvent()
    {
        UIManager.Ins.GetUI<CanvasSkinShop>().OnClickEvent(this);
    }
}
