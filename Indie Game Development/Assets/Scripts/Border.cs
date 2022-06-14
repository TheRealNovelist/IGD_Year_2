using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.OdinInspector;

public class Border : MonoBehaviour
{
    public TileBase tileToPaint;
    public Tilemap map;

    [Button]
    public void AddBorderArea(Vector2Int startingPos, int width, int height)
    {
        for (int x = startingPos.x; x <= startingPos.x + width; x++)
        {
            map.SetTile(new Vector3Int(x, startingPos.y,0), tileToPaint);
            map.SetTile(new Vector3Int(x, startingPos.y + height,0), tileToPaint);
        }
        
        for (int y = startingPos.y; y <= startingPos.y + height; y++)
        {
            map.SetTile(new Vector3Int(startingPos.x, y,0), tileToPaint);
            map.SetTile(new Vector3Int(startingPos.x + width, y,0), tileToPaint);
        }
    }
    
    [Button]
    public void ClearTileArea(Vector2Int startingPos, int width, int height) => map.BoxFill((Vector3Int)startingPos,null, startingPos.x, startingPos.y, startingPos.x + width, startingPos.y + height);

    [Button("Clear All")]
    public void ClearAllBorder() => map.ClearAllTiles();
    
    
}
