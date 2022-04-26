using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BuildCell : MonoBehaviour
{
    [Header("Position")]
    public int xIndex;
    public int yIndex;

    [Header("Property")]
    public bool isBuiltOn = false;
    public BuildType occupiedBuildType = BuildType.None;

    public BuildGrid grid;
    public BuildManager buildManager;

    public Collider2D cellCollider;

    public void Init(int x, int y, BuildGrid grid, BuildManager buildManager)
    {
        xIndex = x;
        yIndex = y;
        this.grid = grid;
        this.buildManager = buildManager;
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on cell (" + xIndex + ", " + yIndex + ")");
        buildManager.Build(xIndex, yIndex, buildManager.selectedRoom);
    }

    public void OnRoomBuild(BuildType occupiedType)
    {
        isBuiltOn = true;
        occupiedBuildType = occupiedType;
        gameObject.SetActive(false);
    }

    public void OnRoomDestroy()
    {
        isBuiltOn = false;
        occupiedBuildType = BuildType.None;
        if (grid.isGridActive)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
