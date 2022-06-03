using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    private RoomSize _preferredRoom;
    private Room _currentRoom;
    
    private List<ServiceType> _currentServices = new List<ServiceType>();

    private void Awake()
    {
        _preferredRoom =  Utility.RandomEnumValue<RoomSize>();
    }

    private void GenerateNewRequest()
    {
        
    }
    public bool IsCurrentRequestFulfilled()
    {
        if (!_currentRoom)
        {
            return _currentRoom;
        }

        return false;
    }
}
