using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public PlayerData playerData = new(3, 5, 5, 2, 5);

    public int weaponId;

    public List<Weapon> weapons = new();

    public Dictionary<ItemType, Item[]> itemList = new();
    public Dictionary<ItemType, int> itemIdList;
    public Dictionary<ItemType, bool[]> itemUnlock;

    [SerializeField] ListItemUnit listItemUnit;

    [ContextMenu("SaveData")]
    void PreSave()
    {
        FileDataHandler.SaveGame(playerData);
    }

    private void Awake()
    {
        itemList = listItemUnit.ToItemDictionary();
        LoadData();

        
        
    }

    public void LoadData()
    {
        playerData = FileDataHandler.LoadGame();

        weaponId = playerData.weaponId;

        itemIdList = new Dictionary<ItemType, int>()
        {
            {ItemType.Hat, playerData.itemId[0]},
            {ItemType.Pant, playerData.itemId[1]},
            {ItemType.Shield, playerData.itemId[2]},
            {ItemType.Suit, playerData.itemId[3]},
        };

        itemUnlock = new Dictionary<ItemType, bool[]>()
        {
            {ItemType.Hat, playerData.hatUnlock},
            {ItemType.Pant, playerData.pantUnlock},
            {ItemType.Shield, playerData.shieldUnlock},
            {ItemType.Suit, playerData.suitUnlock},
        };
    }

    public void SaveData()
    {
        playerData.weaponId = weaponId;

        foreach (ItemType type in itemIdList.Keys)
        {
            playerData.itemId[(int)type] = itemIdList[type];
        }

        playerData.hatUnlock.CopyTo(itemUnlock[ItemType.Hat], 0);
        playerData.pantUnlock.CopyTo(itemUnlock[ItemType.Pant], 0);
        playerData.shieldUnlock.CopyTo(itemUnlock[ItemType.Shield], 0);
        playerData.suitUnlock.CopyTo(itemUnlock[ItemType.Suit], 0);

        FileDataHandler.SaveGame(playerData);
    }

    public Weapon GetCurrentWeapon()
    {
        return weapons[weaponId];
    }
}

public enum WeaponType
{
    Hammer = 3,
    Knife = 4,
    Lollipop = 5
}

public enum ItemType
{
    None = -1,

    Hat = 0,
    Pant = 1,
    Shield = 2,
    Suit = 3
}

[Serializable]
public class ItemUnit
{
    public ItemType type;
    public Item[] items;
}

[Serializable]
public class ListItemUnit
{
    [SerializeField]
    ItemUnit[] itemUnits;

    public Dictionary<ItemType, Item[]> ToItemDictionary()
    {
        Dictionary<ItemType, Item[]> newDict = new();

        foreach (ItemUnit unit in itemUnits)
        {
            newDict.Add(unit.type, unit.items);
        }

        return newDict;
    }
}

[System.Serializable]
public class PlayerData
{
    public string name;
    public int stage;
    public int coin;

    public int weaponId = 0;
    public int[] itemId;

    public bool[] weaponsUnlock;
    public bool[] hatUnlock;
    public bool[] pantUnlock;
    public bool[] shieldUnlock;
    public bool[] suitUnlock;

    public PlayerData(int weaponUnl, int hatUnl, int pantUnl, int shieldUnl, int suitUnl)
    {
        this.itemId = new int[] { -1, -1, -1, -1 };
        this.weaponsUnlock = new bool[weaponUnl];
        this.hatUnlock = new bool[hatUnl];
        this.pantUnlock = new bool[pantUnl];
        this.shieldUnlock = new bool[shieldUnl];
        this.suitUnlock = new bool[suitUnl];

        weaponsUnlock[0] = true;
    }
}