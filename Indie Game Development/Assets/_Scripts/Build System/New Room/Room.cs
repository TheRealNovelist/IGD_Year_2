using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Room : MonoBehaviour
{
    [Header("Base Property")] 
    [InlineEditor] public RoomData roomData;

    [Header("Versions")] 
    [SerializeField] private GameObject upVariant;
    [SerializeField] private GameObject downVariant;
    [Space] 
    private List<Cell> _cellsOccupied;

    public void ConstructRoom(List<Cell> cellToOccupy, FloorLayer layer)
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
        }
        
        _cellsOccupied = new List<Cell>(cellToOccupy);

        foreach (Cell cell in _cellsOccupied)
        {
            cell.OnCellBuilt();
        }
    }

    public void DestroyRoom()
    {
        foreach (Cell cell in _cellsOccupied)
        {
            cell.OnRoomDestroy();
        }
        
        Destroy(gameObject);
    }

    public void OnMouseDown()
    {
        if (BuildingManager.IsDestroying)
        {
            DestroyRoom();
        }
        
        GuestToRoomInput.SetRoom(this);
    }
}
