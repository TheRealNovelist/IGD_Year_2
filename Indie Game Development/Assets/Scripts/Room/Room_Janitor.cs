using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Room_Janitor : Room_Framework
{
    public static event Action OnMaxJanitorChanged;
    public static event Action OnJanitorDeployed;
    public static event Action OnFinishCleaning;

    private static int maxJanitor = 0;
    private static int deployedJanitor = 0;

    private JanitorUI janitorUI;

    private void Start()
    {
        if (!janitorUI)
            janitorUI = FindObjectOfType<JanitorUI>();
    }

    private void OnEnable()
    {
        maxJanitor++;
        Debug.Log("Janitor Amount: " + maxJanitor);
    }

    private void OnDisable()
    {
        if (maxJanitor > 0)
        {
            maxJanitor--;
        }    
        Debug.Log("Janitor Amount: " + maxJanitor);
    }

    private void OnMouseDown()
    {
        janitorUI.OpenPanel();
    }

    public void ChangeMaxJanitorAmount(int amount)
    {
        maxJanitor += amount;
        OnMaxJanitorChanged?.Invoke();
    }

    public static int GetMaxJanitorAmount()
    {
        return maxJanitor;
    }

    public static int GetAvailableJanitor()
    {
        return maxJanitor - deployedJanitor;
    }

    public static bool DeployJanitor()
    {
        if (deployedJanitor <= maxJanitor)
        {
            deployedJanitor++;
            return true;
        }

        return false;
    }

    public static void RetrieveJanitor()
    {
        deployedJanitor--;
    }
}
