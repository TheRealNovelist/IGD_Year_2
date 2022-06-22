using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Room : MonoBehaviour
{
    [Header("Base Property")]
    public RoomSize size = RoomSize.Single;
    public RoomType roomType = RoomType.Guestroom;

    [Header("Versions")] 
    [SerializeField] private GameObject upVariant;
    [SerializeField] private GameObject downVariant;
    [Space] 
    private List<Cell> _cellsOccupied;

    public Vector2Int GetRoomSize() 
    {
        return MyUtility.ConvertRoomSize(size);
    }
    
    public void OnRoomBuilt(List<Cell> cellToOccupy, FloorLayer layer)
    {
        switch (layer)
        {
            case FloorLayer.Up:
                upVariant.SetActive(true);
                downVariant.SetActive(false);
                break;
            case FloorLayer.Down:
                upVariant.SetActive(false);
                downVariant.SetActive(true);
                break;
            case FloorLayer.None:
                Debug.LogWarning("RoomConstructor: Layer is invalid");
                Debug.Break();
                break;
        }
        
        _cellsOccupied = new List<Cell>(cellToOccupy);

        foreach (Cell cell in _cellsOccupied)
        {
            cell.OnCellBuilt();
        }
    }

    public void OnRoomDestroy()
    {
        foreach (Cell cell in _cellsOccupied)
        {
            cell.OnRoomDestroy();
        }
    }

    public void OnMouseDown()
    {
        if (BuildingManager.IsDestroyMode)
        {
            Destroy(gameObject);
        }
        
        GuestToRoomInput.SetRoom(this);
    }
}
