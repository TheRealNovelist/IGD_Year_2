using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GuestToRoomInput
{
    private static Guest _currentGuest;

    public static bool IsCurrentGuest(Guest guest)
    {
        return _currentGuest == guest;
    }
    
    public static void SetGuest(Guest guest)
    {
        if (_currentGuest != null)
        {
            _currentGuest.Deselect();
        }
        
        _currentGuest = guest;
        
        if (_currentGuest != null)
        {
            _currentGuest.Select();
        }
    }

    public static void SetRoom(Room room)
    {
        if (!_currentGuest)
            return;
        
        _currentGuest.SetRoom(room);
        _currentGuest.Deselect();
        _currentGuest = null;
    }
}
