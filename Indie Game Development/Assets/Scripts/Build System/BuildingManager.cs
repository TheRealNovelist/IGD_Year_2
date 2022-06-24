using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static bool IsBuildMode = false;

    #region Room Data

    private static RoomData _currentRoomData;

    public static void SetCurrentRoomData(RoomData roomData)
    {
        _currentRoomData = roomData;
    }

    #endregion

    private List<Cell> _allCellSelected = new List<Cell>();
    private FloorLayer _currentLayerSelected = FloorLayer.None;

    public void Build()
    {
        
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

            if (_allCellSelected.Count == _currentRoomData.size)
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
        if (_allCellSelected.Count == _currentRoomData.size)
        {
                        
            return;
        }
        
        ClearSelection();
    }
    #endregion
}
