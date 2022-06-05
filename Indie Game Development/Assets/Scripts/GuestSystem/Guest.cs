using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    private GuestGenerator _generator;
    
    private RoomSize _preferredRoom;
    private Room _currentRoom;
    
    private List<ServiceType> _currentServices = new List<ServiceType>();

    private void Awake()
    {
        _preferredRoom = Utility.RandomEnumValue<RoomSize>();
    }

    public void Init(GuestGenerator generator)
    {
        _generator = generator;
    }
    
    public bool IsCurrentRequestFulfilled()
    {
        if (_currentRoom == false)
        {
            return false;
        }

        return true;
    }

    public void SetRoom(Room room)
    {
        _currentRoom = room;
        _generator.LeaveQueue();
    }
}
