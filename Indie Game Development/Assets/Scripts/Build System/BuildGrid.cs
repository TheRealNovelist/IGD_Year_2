using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Direction
{
    None,
    Up,
    Right,
    Down,
    Left
}

public class BuildGrid : MonoBehaviour
{
    [SerializeField] private int width, height;

    public int Width
    {
        get
        {
            return width;
        }
        private set
        {
            width = value;
            RegenerateBuildGrid();
        }
    }

    public int Height
    {
        get
        {
            return height;
        }
        private set
        {
            height = value;
            RegenerateBuildGrid();
        }
    }

    [Space]
    public int maxFloor = 5;
    [Space]
    public GameObject cellPrefab;
    private BuildCell[,] cells;

    public bool isGridActive = true;
    [Space]
    public BuildManager buildManager;

    private void Start()
    {
        RegenerateBuildGrid();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isGridActive)
            {
                ToggleGrid(false);
            }
            else
            {
                ToggleGrid(true);
            }
        }
    }

    public void ChangeGridSize(int x = 2, int y = 6)
    {
        width = x;
        height = y;
        RegenerateBuildGrid();
    }

    public void IncreaseGridSize(int x = 0, int y = 0)
    {
        width += x;
        height += y;
        RegenerateBuildGrid();
    }

    public void RegenerateBuildGrid()
    {
        //If the array is null, create a new array of width and height
        if (cells == null)
        {
            cells = new BuildCell[width, height];
        }
        //Else, copy the existing array to a new array to accomodate changes to the base width and height
        else
        {
            BuildCell[,] tempCells = cells;
            cells = new BuildCell[width, height];

            for (int x = 0; x < tempCells.GetLength(0); x++)
            {
                for (int y = 0; y < tempCells.GetLength(1); y++)
                {
                    cells[x, y] = tempCells[x, y];
                }
            }

            Debug.Log("BuildGrid: Rebuilded grid!");
        }

        //Loop the entire array, if any cell in the array is null, populate with a new cell.
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (cells[x, y] == null)
                {
                    GameObject cell = Instantiate(cellPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;

                    //Give name for easy identification
                    cell.name = "Cell (" + x + ", " + y + ")";

                    //Organization within the grid object.
                    cell.gameObject.transform.parent = gameObject.transform;

                    cells[x, y] = cell.GetComponent<BuildCell>();

                    //Passing in the BuildGrid and BuildManager
                    cells[x, y].Init(x, y, this, buildManager);

                    //Check if the entire grid is currently hidden, allowing generating even not visible
                    if (!isGridActive)
                    {
                        ToggleCell(cells[x, y], false);
                    }
                    else
                    {
                        ToggleCell(cells[x, y], true);
                    }

                }
            }
        }
    }

    public void ToggleGrid(bool isActive)
    {
        foreach (BuildCell cell in cells)
        {
            ToggleCell(cell, isActive);
            isGridActive = isActive;
        }
    }

    public void ToggleCell(BuildCell cell, bool isActive)
    {
        if (cell != null && !cell.isBuiltOn)
        {
            cell.gameObject.SetActive(isActive);
        }
    }

    public bool IsCellValid(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //This function double as IsValid check and IsBuiltOn check for cells.
    public bool IsCellBuiltOn(int x, int y)
    {
        //Return true immediately if cell is invalid (count invalid cells as built)
        if (!IsCellValid(x, y))
        {
            return true;
        }
        else
        {
            return cells[x, y].isBuiltOn;
        }
    }

    public List<BuildCell> GetBuildArea(int xIndex, int yIndex, int rightAmount = 1, int upAmount = 1)
    {
        List<BuildCell> allCells = new List<BuildCell>();

        //Defensive mechanism in case wrong indexes were inputed
        if (!IsCellValid(xIndex, yIndex))
        {
            Debug.Log("BuildGrid: Cell (" + xIndex + "," + yIndex + ") is not valid!");
            return new List<BuildCell>();
        }

        for (int x = 0; x < rightAmount; x++)
        {
            for (int y = 0; y < upAmount; y++)
            {
                int coordX = xIndex + x;
                int coordY = yIndex + y;

                if (IsCellBuiltOn(coordX, coordY))
                {
                    Debug.Log("BuildGrid: Cell (" + x + "," + y + ") have not enough space to build!");
                    return new List<BuildCell>();
                }

                allCells.Add(cells[coordX, coordY]);
            }
        }

        return allCells;
    }
    
    public BuildType GetBuildTypeAtIndex(int xIndex, int yIndex)
    {
        if (IsCellValid(xIndex, yIndex))
        {
            return cells[xIndex, yIndex].occupiedBuildType;
        }
        else
        {
            return BuildType.None;
        }
    }

    public Dictionary<Direction, BuildType> GetNeighboringCells(int xIndex, int yIndex)
    {
        Dictionary<Direction, BuildType> neighborType = new Dictionary<Direction, BuildType>();

        //Fallback mechanism in case wrong indexes were inputed
        if (!IsCellValid(xIndex, yIndex))
        {
            Debug.Log("BuildGrid: Cell (" + xIndex + "," + yIndex + ") is not valid!");

            //Add a key-value of each type as none, then return the Dictionary
            neighborType.Add(Direction.None, BuildType.None);
            return neighborType;
        }

        //If the cell is valid, the function will continue, and add the correct direction for each type.

        neighborType[Direction.Up] = GetBuildTypeAtIndex(xIndex, yIndex + 1);
        neighborType[Direction.Right] = GetBuildTypeAtIndex(xIndex + 1, yIndex);
        neighborType[Direction.Down] = GetBuildTypeAtIndex(xIndex, yIndex - 1);
        neighborType[Direction.Left] = GetBuildTypeAtIndex(xIndex - 1, yIndex);

        return neighborType;
    }

    public bool IsNextToBuildType(int xIndex, int yIndex, BuildType typeToCheck)
    {
        Dictionary<Direction, BuildType> neighborCheck = GetNeighboringCells(xIndex, yIndex);

        foreach (KeyValuePair<Direction, BuildType> cellCheck in neighborCheck)
        {
            BuildType buildType = cellCheck.Value;

            if (buildType == typeToCheck)
            {
                return true; 
            }
        }

        return false;
    }

    //Overload to include Directional checks.
    public bool IsNextToBuildType(int xIndex, int yIndex, BuildType typeToCheck, out List<Direction> directions)
    {
        Dictionary<Direction, BuildType> neighborCheck = GetNeighboringCells(xIndex, yIndex);
        List<Direction> directionCheck = new List<Direction>();

        foreach (KeyValuePair<Direction, BuildType> cellCheck in neighborCheck)
        {
            Direction direction = cellCheck.Key;
            BuildType buildType = cellCheck.Value;

            if (buildType == typeToCheck)
            {
                directionCheck.Add(direction);
            }
        }

        //Return list of direction (even when empty)
        directions = directionCheck;

        if (directionCheck.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //Overload version of IsNextToBuildType, using a list of cells to get any corridor next to entire area
    public bool IsNextToBuildType(List<BuildCell> cellsToCheck, BuildType typeToCheck)
    {
        foreach (BuildCell cell in cellsToCheck)
        {
            if (IsNextToBuildType(cell.xIndex, cell.yIndex, typeToCheck))
            {
                return true;
            }
        }

        return false;
    }

    //Overload to include Directional checks
    public bool IsNextToBuildType(List<BuildCell> cellsToCheck, BuildType typeToCheck, out List<Direction> directions)
    {
        List<Direction> directionCheck = new List<Direction>();

        foreach (BuildCell cell in cellsToCheck)
        {
            if (IsNextToBuildType(cell.xIndex, cell.yIndex, typeToCheck, out directionCheck))
            {
                directions = directionCheck;
                return true;
            }
        }

        directions = directionCheck;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector2((float)width / 2, (float)height / 2), new Vector2(width, height));
    }
}
