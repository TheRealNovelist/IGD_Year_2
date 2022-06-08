using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShiftManager : MonoBehaviour
{
    private static List<Guest> allGuests = new List<Guest>();
    public int amountOfGuest;
    public TextMeshProUGUI stateText;
    public GameObject button;

    private void Start()
    {
        stateText.text = "Standby";
    }

    void Update()
    {
        if (allGuests.Count <= 0)
        {
            EndShift();
        }
    }
    
    public void StartShift()
    {
        var guestGen = FindObjectOfType<GuestGenerator>();

        for (int i = 0; i < amountOfGuest; i++)
        {
            Guest guest = guestGen.GenerateGuest();
            allGuests.Add(guest);
        }

        button.SetActive(false);
        stateText.text = "Running";
    }
    

    void EndShift()
    {
        button.SetActive(true);
        stateText.text = "Standby";
    }

    public static void RemoveGuest(Guest guest)
    {
        allGuests.Remove(guest);
    }
}
