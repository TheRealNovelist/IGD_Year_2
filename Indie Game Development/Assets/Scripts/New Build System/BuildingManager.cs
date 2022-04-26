using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RoomPrefabs
{
    [Header("Basic Settings")]
    public string name;
    public Vector2 roomSize = new Vector2(1, 1);
    [Header("Prefabs")]
    public GameObject upPrefab;
    public GameObject downPrefab;
}

public class BuildingManager : MonoBehaviour
{
    public static bool IsBuildMode = false;

    private FloorManager _floorManager;

    [SerializeField] private List<RoomPrefabs> allRoomPrefabs;

    [SerializeField] private List<Cell> allCellSelected;
    [SerializeField] private FloorLayer currentLayerSelected;

    private RoomPrefabs selectedRoomPrefab;

    private void Awake()
    {
        selectedRoomPrefab ??= allRoomPrefabs[0];
        _floorManager ??= GameObject.Find("_FloorManager").GetComponent<FloorManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedRoomPrefab = GetRoomPrefab("Single");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedRoomPrefab = GetRoomPrefab("Quad");
        }

    }

    private RoomPrefabs GetRoomPrefab(string roomName)
    {
        foreach (RoomPrefabs prefab in allRoomPrefabs)
        {
            if (prefab.name == roomName)
            {
                return prefab;
            }
        }

        Debug.LogError("BuildingManager.GetRoomPrefab: Prefab " + roomName + " not found!");
        return null;
    }

    private void SpawnRoom(RoomPrefabs roomPrefabs, FloorLayer layer, List<Cell> cellToOccupy)
    {
        if (roomPrefabs == null)
        {
            Debug.Log("BuildingManager.SpawnRoom: The Room Prefab to build has not been selected");
            return;
        }

        //Temporary default
        GameObject roomToSpawn;
        Vector2 roomSpawnPoint = GetSmallestCell(cellToOccupy).transform.position;

        switch (layer)
        {
            case FloorLayer.Up:
                roomToSpawn = roomPrefabs.upPrefab;
                roomSpawnPoint += roomPrefabs.roomSize * 0.5f;
                break;
            case FloorLayer.Down:
                roomToSpawn = roomPrefabs.downPrefab;
                roomSpawnPoint += new Vector2(0, 1);
                roomSpawnPoint = new Vector2(roomSpawnPoint.x + roomPrefabs.roomSize.x * 0.5f, roomSpawnPoint.y - roomPrefabs.roomSize.y * 0.5f);
                break;
            case FloorLayer.None:
                Debug.LogError("BuildingManager.SpawnRoom: Illegal layer detected");
                Debug.Break();
                return;
            default:
                throw new NullReferenceException("BuildingManager.SpawnRoom: Code directed to an illegal route");
        }

        GameObject newRoom = Instantiate(roomToSpawn, roomSpawnPoint, Quaternion.identity);

        SetUpRoom(newRoom, roomPrefabs.name, cellToOccupy);
    }
    
    private void SetUpRoom(GameObject roomObject, string roomName, List<Cell> cellToOccupy)
    {
        roomObject.name = roomName;
        roomObject.transform.parent = _floorManager.currentFloor.allRooms.transform;

        Room room = roomObject.GetComponent<Room>();
        if (!room)
        {
            Debug.LogError("BuildingManager.SetUpRoom: Object Instantiated did not have script of type Room");
            Debug.Break();
            return;
        }

        room.OnRoomBuilt(cellToOccupy);
    }

    private Cell GetSmallestCell(List<Cell> cells)
    {
        Cell smallestCell = cells[0];
        for (int i = 1; i < cells.Count; i++)
        {
            if (smallestCell.index > cells[i].index)
                smallestCell = cells[i];
        }
        return smallestCell;
    }

    private bool IsRoomSizeReached()
    {
        //Find out if the cell selected aligned with the amount of room size on x axis.
        return allCellSelected.Count == selectedRoomPrefab.roomSize.x;
    }

    private void OnRoomSizeReached()
    {
        SpawnRoom(selectedRoomPrefab, currentLayerSelected, allCellSelected);

        //Call OnCellReleased to reset the selection variables.
        OnCellReleased();
    }

    #region Cell Interaction

    public void OnFirstCellClick(Cell cell)
    {
        if (allCellSelected.Count == 0)
        {
            allCellSelected.Add(cell);
            currentLayerSelected = cell.layer;
        }

        if (IsRoomSizeReached())
        {
            OnRoomSizeReached();
        }
    }

    public void OnAdjacentCellClick(Cell cell)
    {
        if (allCellSelected.Count > 0)
        {
            if (allCellSelected.Contains(cell))
            {
                Debug.Log("Cell exist!");
                return;
            }

            foreach (Cell selectedCell in allCellSelected)
            {
                if (selectedCell.layer != cell.layer)
                {
                    Debug.Log("Wrong Layer!");
                    return;
                }

                if (cell.index == selectedCell.index + 1 || cell.index == selectedCell.index - 1)
                {
                    allCellSelected.Add(cell);
                    if (IsRoomSizeReached())
                    {
                        OnRoomSizeReached();
                    }
                    return;
                }
            }
        }
    }

    public void OnCellReleased()
    {
        allCellSelected.Clear();
        currentLayerSelected = FloorLayer.None;
    }
    #endregion
}
