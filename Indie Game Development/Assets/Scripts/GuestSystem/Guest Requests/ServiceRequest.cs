using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceRequest
{
    private readonly ServiceType _serviceRequested;

    public ServiceRequest(ServiceType newRequest)
    {
        _serviceRequested = newRequest;
    }
    
    public ServiceType GetRequest()
    {
        return _serviceRequested;
    }
    
    public bool Compare(ServiceType serviceToCompare)
    {
        return serviceToCompare == _serviceRequested;
    }
}
