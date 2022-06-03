using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestGenerator : MonoBehaviour
{
    [SerializeField] private GameObject guestPrefab;
    
    
    private void GenerateGuest()
    {
        Instantiate(guestPrefab, gameObject.transform);
    }

    private void LeaveQueue()
    {
        
    }
} 
