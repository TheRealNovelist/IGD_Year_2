using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu(menuName = "RoomSO", fileName = "New Room Setting")]
public class RoomSO : ScriptableObject
{
    [DisableInEditorMode]
    public string roomName = "Single";
    [PreviewField(ObjectFieldAlignment.Left)]
    public Sprite roomIcon;
    public int price = 500;
    public int size = 1;
    public RoomType roomType = RoomType.Guestroom;
    
    [AssetsOnly]
    public GameObject prefab;

#if UNITY_EDITOR
    private void OnValidate()
    {
        string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
        roomName = Path.GetFileNameWithoutExtension(assetPath);
    }
#endif
}
