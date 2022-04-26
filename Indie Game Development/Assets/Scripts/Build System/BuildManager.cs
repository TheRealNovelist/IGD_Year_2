using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildManager : MonoBehaviour
{
    [System.Serializable]
    public struct RoomPlacement
    {
        public Vector2Int position;
        public GameObject prefab;
    }

    [Header("Pre-game Setup")]
    public RoomPlacement[] prefabsToSetup;

    [Header("Build Setup")]
    public GameObject[] roomPrefabs;
    [Space]
    public GameObject selectedRoom;

    [Header("Component")]
    public BuildGrid grid;
    public GuestroomUI uiHandler;
    public TextMeshProUGUI warningText;

    public GameManager gameManager => FindObjectOfType<GameManager>();

    private void Start()
    {
        SetupRoom();
    }

    public void SetupRoom()
    {
        if (prefabsToSetup.Length > 0)
        {
            foreach (RoomPlacement room in prefabsToSetup)
            {
                //Build room set
                Build(room.position.x, room.position.y, room.prefab, true);
            }
        }
    }

    public void SelectRoom(GameObject roomChosen)
    {
        selectedRoom = null;

        //Loop through the roomPrefabs
        foreach (GameObject room in roomPrefabs)
        {
            //Check if the prefab passed in has Room Script
            Room_Framework roomScript = room.GetComponent<Room_Framework>();
            if (roomScript != null)
            {
                //If room type matches the script room type, return out and assign appropriate values.
                if (room == roomChosen)
                {
                    selectedRoom = room;

                    Debug.Log("RoomManager: Selected room of type: " + roomChosen.name.ToString());
                    return;
                }
            }
        }

        //Should the foreach loop through the entire array without a single return, throw error.
        Debug.Log("RoomManager: No prefabs with type " + roomChosen.name.ToString() + " assigned in array!");
    }

    public void Build(int x, int y, GameObject roomToBuild, bool bypassRestriction = false)
    {
        List<Direction> directions;

        Room_Framework roomScript = roomToBuild.GetComponent<Room_Framework>();

        float roomWidth = roomScript.roomSize.x;
        float roomHeight = roomScript.roomSize.y;

        //Cell count of the room (width x height)
        int cellCount = (int)roomWidth * (int)roomHeight;

        //Get cell that need replacing to make space for the room
        List<BuildCell> cellToBuild = grid.GetBuildArea(x, y, roomScript.roomSize.x, roomScript.roomSize.y);
        if (cellToBuild.Count != cellCount)
        {
            //Return when the amount of buildable cells is not equal to the room cell count
            return;
        }

        if (grid.IsNextToBuildType(cellToBuild, BuildType.Corridor, out directions) || bypassRestriction)
        {
            //Money check. If bypass restriction is on, ignore
            if (bypassRestriction == false)
            {
                if (!gameManager.MoneyManager.PayMoney(roomScript.cost))
                {
                    Debug.Log("BuildManager: Not enough money to build the room!");
                    Warning("Not enough money");
                    return;
                }
            }

            //Deactivate each cells that acquired in the GetBuildArea method, passing in the builtType for further reference.
            foreach (BuildCell cell in cellToBuild)
            {
                cell.OnRoomBuild(roomScript.buildType);
            }

            roomScript.cellsOccupied = cellToBuild;

            GameObject newRoom = Instantiate(roomToBuild, new Vector2(x + (roomWidth/2), y + (roomHeight/2)), Quaternion.identity) as GameObject;
            newRoom.transform.parent = gameObject.transform;
            newRoom.name = roomScript.buildType.ToString() + " at (" + x + "," + y + ")";

            if (newRoom.GetComponent<TextureRandomizer>() != null)
            {
                newRoom.GetComponent<TextureRandomizer>().SetSpriteOrientation(directions);
            }

            Debug.Log("BuildManager: Room type " + roomScript.buildType.ToString() + " built!");
        }       
        else
        {
            Debug.Log("BuildManager: Corridor not found");
        }
    }


    public void Warning(string warning)
    {
        StartCoroutine(WarningRoutine(warning));
    }

    IEnumerator WarningRoutine(string warning)
    {
        warningText.text = warning;
        warningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        warningText.gameObject.SetActive(false);
        warningText.text = "";
    }
}
