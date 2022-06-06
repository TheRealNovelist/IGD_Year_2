using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestAssignToRoomInput : MonoBehaviour
{
    private static Guest _currentGuest;
    
    public static void SetGuest(Guest guest)
    {
        _currentGuest = guest;
        Debug.Log("Selected " + guest.name);
    }

    public static void SetRoom(Room room)
    {
        if (!_currentGuest)
            return;

        _currentGuest.transform.position = room.transform.position;
        _currentGuest.SetRoom(room);
        _currentGuest = null;
    }
}
