using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Floor _floor;
    private BuildingManager _buildingManager;

    public Vector2Int cellSize = new Vector2Int(1, 1);

    public int index;
    public FloorLayer layer = FloorLayer.None;

    private bool _isCellOccupied;

    private void Awake()
    {
        if (!_buildingManager)
            _buildingManager = GameObject.Find("_BuildingManager").GetComponent<BuildingManager>();
    }

    public void InitCell(int cellNumber, FloorLayer cellLayer)
    {
        index = cellNumber;
        layer = cellLayer;
    }

    public void SetCellActive(bool active)
    {
        if (!_isCellOccupied)
        {
            gameObject.SetActive(active);
        }
    }

    public void OnCellBuilt()
    {
        SetCellActive(false);
        _isCellOccupied = true;
    }

    public void OnRoomDestroy()
    {
        _isCellOccupied = false;
        if (BuildingManager.IsBuildMode)
        {
            SetCellActive(true);
        }
    }

    #region Input Event
    public void OnMouseDown()
    {
        _buildingManager.OnFirstCellClick(this);
    }

    public void OnMouseEnter()
    {
        _buildingManager.OnAdjacentCellClick(this);
    }

    public void OnMouseUp()
    {
        _buildingManager.OnCellReleased();
    }
    #endregion
   
}
