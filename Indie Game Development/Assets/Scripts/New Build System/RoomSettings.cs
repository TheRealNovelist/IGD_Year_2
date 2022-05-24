using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomPrefab
{
    [Header("Basic Settings")]
    public string name;
    public Vector2 roomSize = new Vector2(1, 1);
    public int cost = 500;
    [Header("Prefabs")]
    public GameObject upPrefab;
    public GameObject downPrefab;
}

[CreateAssetMenu(menuName = "Settings/Room Settings",fileName = "New Room Settings")]
public class RoomSettings : ScriptableObject
{
    public List<RoomPrefab> rooms = new List<RoomPrefab>();

    public RoomPrefab GetRoomPrefab(string name)
    {
        if (rooms.Count == 0)
        {
            Debug.LogWarning("RoomSettings.GetRoomPrefab: No RoomPrefab currently in the List. Please add before continuing!");
            Debug.Break();
        }

        foreach(RoomPrefab room in rooms)
        {
            if (room.name == name)
            {
                return room;
            }
        }

        Debug.LogWarning("RoomSettings.GetRoomPrefab: No RoomPrefab of name " + name + " is found. Returning default prefab");
        return rooms[0];
    }
}
