using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildType
{
    None,
    Corridor,
    Guest,
    HUB,
    Activity,
    Janitor
};

public class Room_Framework : MonoBehaviour
{
    [Header("Building Settings")]
    public Vector2Int roomSize = new Vector2Int(1, 1);
    public BuildType buildType = BuildType.None;
    //public List<BuildCell> cellsOccupied;
    public int cost = 500;

    public void DestroyRoom()
    {
        // foreach (BuildCell cell in cellsOccupied)
        // {
        //     cell.OnRoomDestroy();
        // }

        Destroy(gameObject);
    }
}
