using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Floor _floor;
    private BuildingManager _buildingManager;

    public int index;
    public FloorLayer layer = FloorLayer.None;

    private bool isCellOccupied;

    private void Awake()
    {
        if (!_buildingManager)
            _buildingManager = GameObject.Find("_BuildingManager").GetComponent<BuildingManager>();
    }

    public void InitCell(int cellNumber, FloorLayer cellLayer, Floor floor)
    {
        index = cellNumber;
        layer = cellLayer;

        _floor = floor;
    }

    public void SetCellActive(bool active)
    {
        if (!isCellOccupied)
        {
            gameObject.SetActive(active);
        }
    }

    public void OnCellBuilt()
    {
        SetCellActive(false);
        isCellOccupied = true;
    }

    public void OnRoomDestroy()
    {
        isCellOccupied = false;
        if (BuildingManager.IsBuildMode)
        {
            SetCellActive(true);
        }
    }

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
}
