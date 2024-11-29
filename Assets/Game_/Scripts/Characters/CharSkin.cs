using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSkin : MonoBehaviour
{
    [SerializeField] Character _char;
    [SerializeField] Transform weaponPoint;

    Hat currentHat;
    Pant currentPant;
    Shield currentShield;
    CurrentSuit currentSuit = new(-1);

    [SerializeField] Transform hatPoint;
    [SerializeField] Transform shieldPoint;
    [SerializeField] Renderer pantRenderer;

    [SerializeField] Transform wingPoint;
    public Renderer bodyRenderer;
    [field: SerializeField] public Texture2D DefaultTexture {  get; private set; }

    [System.Serializable]
    public class CurrentSuit
    {
        public int Id;
        public GameObject hat;
        public GameObject shield;
        public GameObject wing;

        public CurrentSuit(int id)
        {
            this.Id = id;
        }
    }

    public void SetDefaultTexture()
    {
        bodyRenderer.material.mainTexture = DefaultTexture;
        bodyRenderer.material.color = _char.color;
    }

    public void GetDefaultSkin()
    {
        if (DataManager.Ins.weaponId >= 0)
        {
            AddWeapon(DataManager.Ins.GetCurrentWeapon());
        }

        Dictionary<ItemType, int> idList = DataManager.Ins.itemIdList;
        foreach (ItemType type in idList.Keys)
        {
            if (idList[type] >= 0)
            {
                AddItem(type, idList[type]);
            }
            else
            {
                RemoveItem(type);
            }
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        if (weapon != _char.currentWeapon)
        {
            if (_char.currentWeapon != null)
            {
                Destroy(_char.currentWeapon.gameObject);
            }
            _char.currentWeapon = Instantiate(weapon, weaponPoint);
        }
    }

    public void AddItem(ItemType type, int id)
    {
        switch (type)
        {
            case ItemType.None:
                break;

            case ItemType.Hat:
                RemoveItem(ItemType.Suit);
                Hat hat = (Hat)DataManager.Ins.itemList[ItemType.Hat][id];
                if (hat != currentHat)
                {
                    RemoveItem(ItemType.Hat);
                    currentHat = Instantiate(hat, hatPoint);
                }
                break;

            case ItemType.Pant:
                RemoveItem(ItemType.Suit);
                Pant pant = (Pant)DataManager.Ins.itemList[ItemType.Pant][id];
                if (pant != currentPant)
                {
                    RemoveItem(ItemType.Pant);
                    currentPant = pant;
                    pantRenderer.gameObject.SetActive(true);
                    pantRenderer.material.mainTexture = currentPant.texture;
                }
                break;

            case ItemType.Shield:
                RemoveItem(ItemType.Suit);
                Shield shield = (Shield)DataManager.Ins.itemList[ItemType.Shield][id];
                if (shield != currentShield)
                {
                    RemoveItem(ItemType.Shield);
                    currentShield = Instantiate(shield, shieldPoint);
                }
                break;

            case ItemType.Suit:
                RemoveItem(ItemType.Hat);
                RemoveItem(ItemType.Pant);
                RemoveItem(ItemType.Shield);

                Suit suit = (Suit)DataManager.Ins.itemList[ItemType.Suit][id];
                if (suit.data.index != currentSuit.Id)
                {
                    RemoveItem(ItemType.Suit);
                    currentSuit.Id = suit.data.index;

                    if (suit.wing != null)
                    {
                        currentSuit.wing = Instantiate(suit.wing, wingPoint);
                    }
                    if (suit.hat != null)
                    {
                        currentSuit.hat = Instantiate(suit.hat, hatPoint);
                    }
                    if (suit.shield != null)
                    {
                        currentSuit.shield = Instantiate(suit.shield, shieldPoint);
                    }
                    bodyRenderer.material.color = Color.white;
                    bodyRenderer.material.mainTexture = suit.texture;
                }
                break;

        }
    }

    public void RemoveItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.None:
                break;
            case ItemType.Hat:
                if (currentHat != null)
                {
                    Destroy(currentHat.gameObject);
                }
                currentHat = null;
                break;
            case ItemType.Pant:
                currentPant = null;
                pantRenderer.gameObject.SetActive(false);
                break;
            case ItemType.Shield:
                if (currentShield != null)
                {
                    Destroy(currentShield.gameObject);
                }
                currentShield = null;
                break;
            case ItemType.Suit:
                if (currentSuit != null)
                {
                    currentSuit.Id = -1;
                    if (currentSuit.wing != null)
                    {
                        Destroy(currentSuit.wing);
                        currentSuit.wing = null;
                    }
                    if (currentSuit.shield != null)
                    {
                        Destroy(currentSuit.shield);
                        currentSuit.shield = null;
                    }
                    if (currentSuit.hat != null)
                    {
                        Destroy(currentSuit.hat);
                        currentSuit.hat = null;
                    }
                }

                SetDefaultTexture();
                break;
        }
    }
}
