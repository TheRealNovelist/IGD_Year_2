using System;
using UnityEngine;
using System.Collections.Generic;

public enum RoomSize
{
    Single,
    Double,
    Quad
}

public static class Utility
{
    public static T RandomEnumValue<T>()
    {
        var values = Enum.GetValues(typeof(T));
        int random = UnityEngine.Random.Range(0, values.Length);
        return (T)values.GetValue(random);
    }
    
    public static Vector2Int ConvertRoomSize(RoomSize size)
    {
        switch (size)
        {
            default:                    return new Vector2Int(1, 1);
            case RoomSize.Single:       return new Vector2Int(1, 1);
            case RoomSize.Double:       return new Vector2Int(2, 1);
            case RoomSize.Quad:         return new Vector2Int(2, 2);
        }
    }
}