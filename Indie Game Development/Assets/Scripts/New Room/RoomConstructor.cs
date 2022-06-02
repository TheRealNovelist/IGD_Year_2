using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomConstructor : MonoBehaviour
{
    [Header("Base Property")]
    public Vector2Int roomSize = new Vector2Int(1, 1);
    public int cost = 500;
    public GuestroomSize size = GuestroomSize.Single;

    [Header("Versions")] 
    [SerializeField] private GameObject upVariant;
    [SerializeField] private GameObject downVariant;
    [Space] 
    private List<Cell> cellsOccupied;

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
        
        cellsOccupied = new List<Cell>(cellToOccupy);

        foreach (Cell cell in cellsOccupied)
        {
            cell.OnCellBuilt();
        }
    }

    public void OnRoomDestroy()
    {
        foreach (Cell cell in cellsOccupied)
        {
            cell.OnRoomDestroy();
        }
    }
}
