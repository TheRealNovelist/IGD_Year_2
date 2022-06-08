using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestGenerator : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject guestPrefab;
    
    private List<Guest> _queuedGuests = new List<Guest>();
    
    //Queue
    private readonly List<Transform> _visibleQueuePoints = new List<Transform>();
    private Guest[] _visibleGuests;

    private void Awake()
    {
        foreach (Transform queuePoint in transform)
        {
            _visibleQueuePoints.Add(queuePoint);
        }
        
        _visibleGuests = new Guest[_visibleQueuePoints.Count];
    }

    public Guest GenerateGuest()
    {
        GameObject newGuest = Instantiate(guestPrefab);
        newGuest.SetActive(false);
        
        var guest = newGuest.GetComponent<Guest>();
        
        if (!IsVisibleQueueFull())
        {
            MoveToVisibleQueue(guest);
        }
        else
        {
            _queuedGuests.Add(guest);
        }
        
        return guest;
    }

    private void MoveToVisibleQueue(Guest guest)
    {
        for (int i = 0; i < _visibleQueuePoints.Count; i++)
        {
            if (_visibleGuests[i] != null)
                continue;

            _queuedGuests.Remove(guest);
            _visibleGuests[i] = guest;
            
            guest.transform.position = _visibleQueuePoints[i].transform.position;
            guest.gameObject.SetActive(true);
            guest.Init(this);
            break;
        }
    }

    private bool IsVisibleQueueFull()
    {
        int fullness = 0;
        foreach (var t in _visibleGuests)
        {
            if (t != null)
                fullness++;
        }
        
        return fullness == _visibleQueuePoints.Count;
    }
    
    public void LeaveQueue(Guest guest)
    {
        for (int i = 0; i < _visibleGuests.Length; i++)
        {
            if (_visibleGuests[i] == guest)
            {
                _visibleGuests[i] = null;
                break;
            }
        }
        
        if (_queuedGuests.Count > 0)
        {
            MoveToVisibleQueue(_queuedGuests[0]);
        }
    }
} 
