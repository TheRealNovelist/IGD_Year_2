using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(GuestMoodBehaviour))]
public class Guest : MonoBehaviour
{
    [Header("Default Settings")]
    [SerializeField] private int maxServices = 1;
    
    private GuestGenerator _generator;
    private GuestMoodBehaviour _mood;
    private GuestStateMachine _stateMachine;
    
    private RoomSize _preferredRoom;
    private Room _currentRoom;
    
    private int _serviceNumber;
    private List<ServiceType> _currentServices = new List<ServiceType>();

    private void Awake()
    {
        _preferredRoom = Utility.RandomEnumValue<RoomSize>();
        _serviceNumber = Random.Range(1, maxServices);
        
        _mood = GetComponent<GuestMoodBehaviour>();
        _stateMachine = GetComponent<GuestStateMachine>();
    }

    public void Init(GuestGenerator generator)
    {
        _generator = generator;
        _stateMachine.StartStateMachine();
    }

    public void AddNewService()
    {
        if (!_currentRoom)
            return;
        
        for (int i = 0; i < _serviceNumber; i++)
        {
            _currentServices.Add(Utility.RandomEnumValue<ServiceType>());
        }
    }

    public void ProvideService()
    {
        if (_currentServices.Count > 0)
        {
            _currentServices.RemoveAt(0);
        }
    }
    
    public bool IsCurrentRequestFulfilled()
    {
        if (!_currentRoom)
        {
            return false;
        }

        if (_currentServices.Count > 0)
        {
            return false;
        }
        
        return true;
    }

    public void SetRoom(Room room)
    {
        _currentRoom = room;
        transform.position = room.transform.position;
        if (_currentRoom.size == _preferredRoom)
        {
            _mood.AddMood(+1);
        }
        _generator.LeaveQueue(this);
    }

    private void OnMouseDown()
    {
        if (!GuestToRoomInput.IsCurrentGuest(this))
        {
            GuestToRoomInput.SetGuest(this);
        }
        else
        {
            GuestToRoomInput.SetGuest(null);
        }
    }
}
