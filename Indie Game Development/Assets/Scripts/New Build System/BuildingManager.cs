using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BuildingManager : MonoBehaviour
{
    public static bool IsBuildMode = false;
    public static bool IsDestroyMode = false;

    private FloorManager _floorManager;

    [SerializeField] private RoomSettings _roomSettings;
    
    private List<Cell> allCellSelected;
    private FloorLayer currentLayerSelected;

    private RoomPrefab _selectedRoomPrefab;

    private void Awake()
    {
        _floorManager ??= GameObject.Find("_FloorManager").GetComponent<FloorManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _selectedRoomPrefab = _roomSettings.GetRoomPrefab("Single");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _selectedRoomPrefab = _roomSettings.GetRoomPrefab("Quad")
        }

    }

    private void SpawnRoom(RoomPrefab roomPrefabs, FloorLayer layer, List<Cell> cellToOccupy)
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


    #region Cell Interaction

    public void SelectCell(Cell cell)
    {

    }

    public void OnFirstCellClick(Cell cell)
    {
        if (allCellSelected.Count == 0)
        {
            allCellSelected.Add(cell);
            currentLayerSelected = cell.layer;
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
