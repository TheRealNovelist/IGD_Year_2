using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public enum RoomSize
{
    Single,
    Double,
    Quad
}

public static class MyUtility
{
    public static T RandomEnumValue<T>()
    {
        var values = Enum.GetValues(typeof(T));
        int random = UnityEngine.Random.Range(0, values.Length);
        return (T)values.GetValue(random);
    }
    
    public static T RandomEnumValue<T>(List<T> excludes)
    {
        var values = (T[])Enum.GetValues(typeof(T));
        var availableValues = values.Except(excludes).ToArray();
        int random = UnityEngine.Random.Range(0, availableValues.Length);
        return availableValues[random];
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

    public static void SetText(TextMeshProUGUI textMesh, string text)
    {
        if (text != null)
        {
            textMesh.text = text;
        }
    }
}