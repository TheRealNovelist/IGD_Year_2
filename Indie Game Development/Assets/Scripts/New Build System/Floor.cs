using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FloorLayer
{
    None = 0,
    Up = 1,
    Down = -1
}

/// <summary>
/// Manage one grid floor of the hotel.
/// </summary>
public class Floor : MonoBehaviour
{
    private FloorManager _floorManager;

    [SerializeField] private int _floorId = 0;

    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Corridor corridor;

    [SerializeField] private List<Cell> allCells; //Storing all reference for activating grid

    [HideInInspector] public GameObject allRooms;

    public void InitFloor(FloorManager floorManager, int floorId, int floorLength = 6)
    {
        _floorId = floorId;
        _floorManager = floorManager;

        GenerateFloorContent(floorLength);
    }

    private void GenerateFloorContent(int floorLength)
    {
        Transform floorTransform = transform;
        Vector3 floorPosition = floorTransform.position;
        
        GameObject upperLayer = new GameObject("Upper Cell Layer")  { transform = { position = floorPosition, parent = floorTransform}};
        GameObject lowerLayer = new GameObject("Lower Cell Layer")  { transform = { position = floorPosition, parent = floorTransform}};

        allRooms = new GameObject("All Rooms") { transform = { position = floorPosition, parent = floorTransform}};
        
        float spawnOffset = floorPosition.y;
        
        corridor.SetCorridorSize(floorLength);
        
        for (int i = 0; i < floorLength; i++)
        {
            GenerateCell(i, 1, upperLayer, spawnOffset);
            GenerateCell(i, -1, lowerLayer, spawnOffset);
        }
    }
    
    private void GenerateCell(int x, int y, GameObject parentObj = null, float spawnOffset = 0f)
    {
        if (!cellPrefab)
        {
            Debug.Log("FloorScript: Please put in the appropriate CELL PREFAB");
            Debug.Break();
        }

        if (!cellPrefab.GetComponent<Cell>())
        {
            Debug.Log("FloorScript: CELL PREFAB do not have Cell script");
            Debug.Break();
        }

        GameObject newCell = Instantiate(cellPrefab, new Vector3(x, y + spawnOffset, 0), Quaternion.identity);
        Cell cellScript = newCell.GetComponent<Cell>();

        cellScript.InitCell(x, (FloorLayer)y, this);

        newCell.name = "Buildable Cell at " + "(" + x + ", " + y + ")";

        allCells.Add(cellScript);
        cellScript.SetCellActive(BuildingManager.IsBuildMode);

        //Check if a parent object is assigned, make this a child of that object
        if (parentObj != null)
        {
            newCell.transform.parent = parentObj.transform;
        }
    }

    public void SetGridActive (bool active)
    {
        foreach (Cell cell in allCells)
        {
            cell.SetCellActive(active);
        }
    }
}
