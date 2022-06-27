using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class FloorManager : MonoBehaviour
{
    [Header("Settings")]
    public int maxFloorCount = 5;
    public float distanceBetweenFloors = 5f;
    public float floorSwapTime = 2f;

    [SerializeField] private GameObject floorPrefab;

    private int _currentFloorIndex = 0;
    
    //Private variables
    private bool _isSwapping = false;
    private GameObject _floorStorage;
    
    public List<Floor> allFloors;

    public static Floor currentFloor;

    public List<GameObject> swapButton;
    
    private void Awake()
    {
        allFloors = new List<Floor>();
        _floorStorage = new GameObject("All Floors");
        currentFloor = SpawnFloor(5, new Vector3(0, 0, 0));
    }

    private Floor SpawnFloor(int floorLength, Vector3 originPoint, bool swapOnSpawn = false)
    {
        if (allFloors.Count == maxFloorCount)
        {
            Debug.Log("Reached maximum floors");
            return currentFloor;
        }
        
        if (!floorPrefab)
        {
            Debug.Log("[FloorManager] Floor Prefab is not assigned");
            Debug.Break(); //Pause the editor, will be able to resume if the prefab is added.
        }

        //Handle hierarchy of the game object
        GameObject newFloor = Instantiate(floorPrefab, originPoint, Quaternion.identity);
        newFloor.transform.parent = _floorStorage.transform;        

        //Handle script side.
        Floor floorScript = newFloor.GetComponent<Floor>();
        if (!floorScript)
        {
            Debug.Log("[FloorManager] Floor Prefab does not contain Floor Component");
            Debug.Break(); //Pause the editor
        }
        
        allFloors.Add(floorScript);
        int floorIndex = allFloors.IndexOf(floorScript);
        
        //Set up floor
        newFloor.gameObject.name = "Floor " + floorIndex;
        floorScript.InitFloor(this, floorIndex, floorLength);
        
        //Allow swap to newly instantiated floor 
        if (swapOnSpawn) 
            SwapFloor(floorIndex);

        return floorScript;
    }

    //Use for swapping from current floor to a desired floor
    private void SwapFloor(int newFloorIndex)
    {
        if (_isSwapping)
        {
            Debug.Log("FloorManager.SwapFloor: Currently swapping");
            return;
        }
        
        //Check if the new floor is valid
        if (newFloorIndex < 0 || newFloorIndex >= allFloors.Count)
        {
            Debug.Log("FloorManager.SwapFloor: Floor " + newFloorIndex + " is not valid");
            return;
        }

        if (newFloorIndex == _currentFloorIndex)
        {
            Debug.Log("FloorManager.SwapFloor: Target floor is current floor!");
            return;
        }
        
        //Calculate distance to move the floors
        float distanceToMove = (_currentFloorIndex - newFloorIndex) * distanceBetweenFloors;
        float newYPos = _floorStorage.transform.position.y + distanceToMove;

        foreach (GameObject button in swapButton)
        {
            if (button)
            {
                button.SetActive(false);
            }
        }
        
        //Avoid overlapping tween
        _isSwapping = true;
        _floorStorage.transform.DOMoveY(newYPos, floorSwapTime).OnComplete(() => StopSwapping(newFloorIndex));

        if (BuildingManager.IsBuildMode)
        {
            currentFloor.SetGridActive(false);
        }
        
        currentFloor = allFloors[newFloorIndex];
    }

    public void IncrementFloor(int increment)
    {
        SwapFloor(_currentFloorIndex + increment);
    }

    //Callback for when swapping tween is completed
    private void StopSwapping(int newFloor)
    {
        _isSwapping = false;
        _currentFloorIndex = newFloor;
        
        if (BuildingManager.IsBuildMode)
        {
            currentFloor.SetGridActive(true);
        }
        
        foreach (GameObject button in swapButton)
        {
            if (button)
            {
                button.SetActive(true);
            }
        }
    }
}
