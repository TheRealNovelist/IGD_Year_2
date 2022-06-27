using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static bool IsBuildMode = false;
    public static bool IsDestroying = false;

    #region Room Data

    private static RoomData _currentRoomData;

    public static void SetCurrentRoomData(RoomData roomData)
    {
        _currentRoomData = roomData;
    }

    #endregion

    private List<Cell> _allCellSelected = new List<Cell>();
    private FloorLayer _currentLayerSelected = FloorLayer.None;
    
    public static void ActivateBuildMode(bool isActive)
    {
        FloorManager.currentFloor.SetGridActive(isActive);
        IsBuildMode = isActive;
    }

    public static void ActivateDestroyMode(bool isActive)
    {
        IsDestroying = isActive;
    }
    
    public void Build()
    {
        Vector2 roomSpawnPoint = MyUtility.GetSmallestCell(_allCellSelected).transform.position;
        
        switch (_currentLayerSelected)
        {
            case FloorLayer.Up:
                roomSpawnPoint += new Vector2(_currentRoomData.width, _currentRoomData.height) * 0.5f;
                break;
            case FloorLayer.Down:
                roomSpawnPoint += new Vector2(0, 1);
                roomSpawnPoint = new Vector2(roomSpawnPoint.x + _currentRoomData.width * 0.5f, roomSpawnPoint.y - _currentRoomData.height * 0.5f);
                break;
            case FloorLayer.None:
                Debug.LogError("BuildingManager.SpawnRoom: Illegal layer detected");
                Debug.Break();
                return;
            default:
                throw new NullReferenceException("BuildingManager.SpawnRoom: Code directed to an illegal route");
        }

        GameObject newRoom = Instantiate(_currentRoomData.prefab, roomSpawnPoint, Quaternion.identity);
        newRoom.transform.parent = FloorManager.currentFloor.transform;
        newRoom.GetComponent<Room>().ConstructRoom(_allCellSelected, _currentLayerSelected);
    }
    
    #region Cell Interaction
    public void ClearSelection()
    {
        _allCellSelected.Clear();
        _currentLayerSelected = FloorLayer.None;
    }
    
    public void OnFirstCellClick(Cell cell)
    {
        if (!_currentRoomData) { return; }
        
        if (_allCellSelected.Count == 0)
        {
            _currentLayerSelected = cell.layer;
            _allCellSelected.Add(cell);
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

            if (cell.layer != _currentLayerSelected)
            {
                Debug.Log("Cell not belong to current layer!");
            }

            if (_allCellSelected.Count == _currentRoomData.width)
            {
                return;
            }
            
            foreach (Cell selectedCell in _allCellSelected)
            {
                if (cell.index == selectedCell.index + 1 || cell.index == selectedCell.index - 1)
                {
                    _allCellSelected.Add(cell);
                    return;
                }
            }
        }
    }

    public void OnCellReleased()
    {
        if (!_currentRoomData) { return; }
        
        if (_allCellSelected.Count == _currentRoomData.width)
        {
            Build();
        }
        
        ClearSelection();
    }
    #endregion
}
