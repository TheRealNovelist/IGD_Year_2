using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

public enum RoomType
{
    Guestroom,
    Service
}

public enum ServiceType
{
    None,
    Bathroom,
    Restaurant
}

public class DataManager : MonoBehaviour
{
    [SerializeField] private Reputation _reputation;
    [SerializeField] private Money _money;
    
    [SerializeField] private string path = "";

    private void Start()
    {
        LoadData();
    }

    [Button]
    private void CreatePath()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }
    
    public void GetData(PlayerData data)
    {
        _reputation.RetrieveData(out data.level, out data.experience);
        data.money = _money.GetCurrentMoney();
    }

    public void SetData(PlayerData data)
    {
        _reputation.LoadReputation(data.level, data.experience);
    }

    [Button]
    private void SaveData()
    {
        string savePath = path;

        PlayerData playerData = new PlayerData();
        GetData(playerData);
        
        Debug.Log("Saving Data at " + savePath);
        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    [Button]
    private void LoadData()
    {
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        
        SetData(data);
    }
}

public class PlayerData
{
    public int level;
    public int experience;

    public int money;
}