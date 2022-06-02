using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingManager : MonoBehaviour
{
    public static bool IsBuildMode = false;
    public static bool IsDestroyMode = false;
    
    [SerializeField] private List<GameObject> allRoomPrefabs;

    private GameObject _selectedRoomPrefab;
    
    private FloorManager _floorManager;
    private List<Cell> _allCellSelected = new List<Cell>();
    private FloorLayer _currentLayerSelected;


    private void Awake()
    {
        _selectedRoomPrefab ??= allRoomPrefabs[0];
        _floorManager ??= GameObject.Find("_FloorManager").GetComponent<FloorManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _selectedRoomPrefab = GetRoomPrefab("Single");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _selectedRoomPrefab = GetRoomPrefab("Quad");
        }

    }

    private GameObject GetRoomPrefab(string roomName)
    {
        foreach (GameObject prefab in allRoomPrefabs)
        {
            if (prefab.name == roomName)
            {
                //Validate first if the prefab is ok to use.
                return prefab;
            }
        }

        Debug.LogError("BuildingManager.GetRoomPrefab: Prefab " + roomName + " not found!");
        return null;
    }

    private void SpawnRoom(GameObject roomPrefab, FloorLayer layer, List<Cell> cellToOccupy)
    {
        if (roomPrefab == null)
        {
            Debug.Log("BuildingManager.SpawnRoom: The Room Prefab to build has not been selected");
            return;
        }

        RoomConstructor constructor = roomPrefab.GetComponent<RoomConstructor>();
        Vector2 roomSpawnPoint = GetSmallestCell(cellToOccupy).transform.position;

        switch (layer)
        {
            case FloorLayer.Up:
                roomSpawnPoint += (Vector2)constructor.roomSize * 0.5f;
                break;
            case FloorLayer.Down:
                roomSpawnPoint += new Vector2(0, 1);
                roomSpawnPoint = new Vector2(roomSpawnPoint.x + constructor.roomSize.x * 0.5f, roomSpawnPoint.y - constructor.roomSize.y * 0.5f);
                break;
            case FloorLayer.None:
                Debug.LogError("BuildingManager.SpawnRoom: Illegal layer detected");
                Debug.Break();
                return;
            default:
                throw new NullReferenceException("BuildingManager.SpawnRoom: Code directed to an illegal route");
        }

        GameObject newRoom = Instantiate(roomPrefab, roomSpawnPoint, Quaternion.identity);
        newRoom.transform.parent = _floorManager.currentFloor.allRooms.transform;
        newRoom.GetComponent<RoomConstructor>().OnRoomBuilt(cellToOccupy, layer);
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
        return _allCellSelected.Count == (int)_selectedRoomPrefab.GetComponent<RoomConstructor>().roomSize.x;
    }

    private void OnRoomSizeReached()
    {
        SpawnRoom(_selectedRoomPrefab, _currentLayerSelected, _allCellSelected);

        //Call OnCellReleased to reset the selection variables.
        OnCellReleased();
    }

    #region Cell Interaction

    public void OnFirstCellClick(Cell cell)
    {
        if (_allCellSelected.Count == 0)
        {
            _currentLayerSelected = cell.layer;
            _allCellSelected.Add(cell);
            if (IsRoomSizeReached())
            {
                OnRoomSizeReached();
            }
        }
    }

    public void OnAdjacentCellClick(Cell cell)
    {
        if (_allCellSelected.Count > 0)
        {
            if (_allCellSelected.Contains(cell))
            {
                Debug.Log("Cell exist!");
                return;
            }

            foreach (Cell selectedCell in _allCellSelected)
            {
                if (selectedCell.layer != cell.layer)
                {
                    Debug.Log("Wrong Layer!");
                    return;
                }

                if (cell.index == selectedCell.index + 1 || cell.index == selectedCell.index - 1)
                {
                    _allCellSelected.Add(cell);
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
        _allCellSelected.Clear();
        _currentLayerSelected = FloorLayer.None;
    }
    #endregion
}
