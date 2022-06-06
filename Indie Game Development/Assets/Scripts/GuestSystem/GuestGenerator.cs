using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestGenerator : MonoBehaviour
{
    [SerializeField] private GameObject guestPrefab;

    private Queue<Guest> guestQueue = new Queue<Guest>();

    private void Start()
    {
        StartCoroutine(GenerateGuest(0f));
    }

    private IEnumerator GenerateGuest(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        GameObject newGuest = Instantiate(guestPrefab, gameObject.transform);
        var guest = newGuest.GetComponent<Guest>();
        guest.Init(this);
        guestQueue.Enqueue(guest);
    }

    public void LeaveQueue()
    {
        guestQueue.Dequeue();
        StartCoroutine(GenerateGuest(3f));
    }

    private void OnMouseDown()
    {
        if (guestQueue.Count > 0)
        {
            GuestAssignToRoomInput.SetGuest(guestQueue.Peek());
        }
    }
} 
