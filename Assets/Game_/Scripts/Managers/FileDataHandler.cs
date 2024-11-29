using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileDataHandler
{
    public static void SaveGame(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/playerData.json";
        System.IO.File.WriteAllText(path, json);
    }

    public static PlayerData LoadGame()
    {
        string path = Application.persistentDataPath + "/playerData.json";
        if(File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

            return loadedData;
        }
        else
        {
            PlayerData data = new (3, 5, 5, 2, 5);
            SaveGame(data);
            Debug.LogWarning("File not found");
            return data;
        }
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
        this.itemId = new int[] { -1, -1, -1, -1};
        this.weaponsUnlock = new bool[weaponUnl];
        this.hatUnlock = new bool[hatUnl];
        this.pantUnlock = new bool[pantUnl];
        this.shieldUnlock = new bool[shieldUnl];
        this.suitUnlock = new bool[suitUnl];

        weaponsUnlock[0] = true;
    }
}


