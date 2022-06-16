using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

[RequireComponent(typeof(GuestMoodBehaviour))]
public class Guest : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private SpriteRenderer sprite;

    public TextMeshProUGUI statusText;
    
    [Header("Default Settings")]
    [SerializeField] private int maxServices = 1;
    
    private GuestGenerator _generator;
    private GuestMoodBehaviour _mood;
    private GuestStateMachine _stateMachine;
    
    private RoomSize _preferredRoom;
    private Room _currentRoom;
    
    private int _currentServiceAmount;
    private ServiceType _currentRequestedService = ServiceType.None;
    
    private void Awake()
    {
        _preferredRoom = MyUtility.RandomEnumValue<RoomSize>();
        statusText.text = _preferredRoom.ToString();
        _currentServiceAmount = Random.Range(1, maxServices);
        
        _mood = GetComponent<GuestMoodBehaviour>();
        _stateMachine = GetComponent<GuestStateMachine>();
    }

    public void Init(GuestGenerator generator)
    {
        _generator = generator;
        _stateMachine.StartStateMachine();
    }

    public void RequestNewService()
    {
        if (!_currentRoom)
            return;

        var exclude = new List<ServiceType> { ServiceType.None };
        _currentRequestedService = MyUtility.RandomEnumValue(exclude);
        statusText.text = _currentRequestedService.ToString();
    }

    public void ProvideService(ServiceType service)
    {
        if (service != _currentRequestedService)
        {
            Debug.Log("Service input does not match required service");
            return;
        }

        _currentRequestedService = ServiceType.None;
        _currentServiceAmount -= 1;
        
        _mood.AddMood(1);
    }
    
    public bool IsCurrentRequestFulfilled()
    {
        if (!_currentRoom)
        {
            return false;
        }

        return _currentRequestedService == ServiceType.None;
    }

    public bool IsCheckingOut()
    {
        return _currentServiceAmount == 0;
    }

    public void SetRoom(Room room)
    {
        if (_currentRoom)
            return;
        
        _currentRoom = room;
        var roomTransform = room.transform;
        transform.position = roomTransform.position;
        transform.parent = roomTransform;
        
        if (_currentRoom.size == _preferredRoom)
        {
            _mood.AddMood(+1);
        }
        _generator.LeaveQueue(this);
    }

    private void OnMouseDown()
    {
        GuestToRoomInput.SetGuest(!GuestToRoomInput.IsCurrentGuest(this) ? this : null);
    }

    public void Select()
    {
        sprite.color = Color.red;
    }

    public void Deselect()
    {
        sprite.color = Color.white;
    }
}
