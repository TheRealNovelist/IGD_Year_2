using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GuestroomSize
{
    None,
    //1x1
    Single,
    //1x2
    Double,
    //2x2
    Family
}

public enum ServiceType
{
    None,
    Bathroom,
    Restaurant
}



public class DataManager : MonoBehaviour
{
    private List<GuestroomSize> unlockedGuestroomSizes;
    private List<ServiceType> unlockedServiceTypes;

    public void Awake()
    {
        unlockedGuestroomSizes = new List<GuestroomSize>();
        unlockedServiceTypes = new List<ServiceType>();
    }
}
