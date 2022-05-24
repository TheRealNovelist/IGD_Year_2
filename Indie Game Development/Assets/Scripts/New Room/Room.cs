using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Room : MonoBehaviour
{
    [Header("Base Property")]
    public Vector2Int roomSize = new Vector2Int(1, 1);
    public int cost = 500;
    public GuestroomSize size = GuestroomSize.Single;
    [Space]
    private List<Cell> cellsOccupied;

    public void OnRoomBuilt(List<Cell> cellToOccupy)
    {
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
