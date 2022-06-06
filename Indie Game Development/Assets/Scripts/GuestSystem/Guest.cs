using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(GuestMoodBehaviour))]
public class Guest : MonoBehaviour
{
    [Header("Default Settings")]
    [SerializeField] private int maxServices;
    
    private GuestGenerator _generator;
    private GuestMoodBehaviour _mood;
    
    private RoomSize _preferredRoom;
    private Room _currentRoom;
    
    private int _serviceNumber;
    private List<ServiceType> _currentServices = new List<ServiceType>();

    private void Awake()
    {
        _preferredRoom = Utility.RandomEnumValue<RoomSize>();
        _serviceNumber = Random.Range(1, maxServices);
        for (int i = 0; i < _serviceNumber; i++)
        {
            _currentServices.Add(Utility.RandomEnumValue<ServiceType>());
        }
        _mood = GetComponent<GuestMoodBehaviour>();
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
        if (_currentRoom.size == _preferredRoom)
        {
            _mood.AddMood(+1);
        }
        _generator.LeaveQueue();
    }
}
