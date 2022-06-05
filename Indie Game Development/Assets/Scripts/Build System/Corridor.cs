using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : MonoBehaviour
{
    private SpriteRenderer corridor;

    private void Awake()
    {
        if (!corridor) corridor = GetComponent<SpriteRenderer>();
        corridor.drawMode = SpriteDrawMode.Tiled;
    }

    public void SetCorridorSize(int newWidth = 0, int newHeight = 1)
    {
        corridor.size = new Vector2(newWidth, newHeight);
    }
}
