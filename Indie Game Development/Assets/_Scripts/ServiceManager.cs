using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ServiceManager : MonoBehaviour
{
    public static ServiceType CurrentServiceType;

    public TextMeshProUGUI currentServiceText;
    
    public void SetCurrentService(string type)
    {
        switch (type)
        {
            case "Restaurant":
                CurrentServiceType = ServiceType.Restaurant;
                break;
            case "Bathroom":
                CurrentServiceType = ServiceType.Bathroom;
                break;
        }
        currentServiceText.text = CurrentServiceType.ToString();
    }
}
