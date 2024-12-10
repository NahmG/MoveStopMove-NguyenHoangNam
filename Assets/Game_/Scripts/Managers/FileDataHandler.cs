using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class FileDataHandler
{
    static string SAVE_PATH = Application.persistentDataPath + "/playerData.json";
    static bool isInit = false;

    public static void Init()
    {
        if (!isInit)
        {
            isInit = true;
            if (File.Exists(SAVE_PATH))
            {
                PlayerData data = new(3, 5, 5, 2, 5);
                SaveGame(data);
            }
        }
    }

    public static void SaveGame(PlayerData data)
    {
        Init();
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(SAVE_PATH, json);
    }

    public static PlayerData LoadGame()
    {
        Init();
        string json = System.IO.File.ReadAllText(SAVE_PATH);
        PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

        return loadedData;
    }
}




