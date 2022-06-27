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
    //Return a random enum value of type passed in
    public static T RandomEnumValue<T>(List<T> excludes = null)
    {
        //Get all enum values
        var values = (T[])Enum.GetValues(typeof(T));
        
        //Calculate values that are excluded in parameter. Default is null, which mean the method will take all values into account. 
        var availableValues = excludes != null ? values.Except(excludes).ToArray() : values;
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

    public static Cell GetSmallestCell(List<Cell> cells)
    {
        Cell smallestCell = cells[0];
        for (int i = 1; i < cells.Count; i++)
        {
            if (smallestCell.index > cells[i].index)
                smallestCell = cells[i];
        }
        return smallestCell;
    }
}