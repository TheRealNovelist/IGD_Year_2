using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Room Data", fileName = "New Room Setting")]
public class RoomData : Unlockable 
{
    [TitleGroup("Property Data")]
    [DisableInEditorMode]
    public string roomName = "Single";
    [PreviewField(ObjectFieldAlignment.Left)]
    public Sprite roomIcon;
    public int price = 500;
    
    [TitleGroup("Construction Data")]
    [HorizontalGroup]
    public int width = 1, height = 1;
    public RoomType roomType = RoomType.Guestroom;
    
    [AssetsOnly]
    public GameObject prefab;

    public bool isLocked = false;

    public override void Lock()
    {
        base.Lock();
        
        isLocked = true;
    }
    
    public override void Unlock()
    {
        base.Unlock();

        isLocked = false;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        roomName = Path.GetFileNameWithoutExtension(assetPath);
    }
#endif
}
