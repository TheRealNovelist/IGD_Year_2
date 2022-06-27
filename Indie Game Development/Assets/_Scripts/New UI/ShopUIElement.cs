using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

public class ShopUIElement : MonoBehaviour, IPointerClickHandler
{
    private ShopUIManager _manager;

    [TitleGroup("Shop Item")] 
    [InlineButton("SetFromData", "Refresh")]
    [SerializeField] private RoomData roomData;
    
    [TitleGroup("Text")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI cellText;

    [TitleGroup("Icon")]
    [SerializeField] private Image iconImage;
    [SerializeField] private Image iconBackground;

    [TitleGroup("Background")] 
    [SerializeField] private Image background;
    
    [TitleGroup("Lock Mode")]
    [InlineButton("FlipFlopLockState", "Lock/Unlock")]
    [SerializeField] private GameObject lockElement; 
    [HideInInspector] public bool isElementLocked = false;

    public void Init(ShopUIManager manager)
    {
        _manager = manager;
    }

    public RoomData GetRoomData()
    {
        return roomData;
    }

    private void OnEnable()
    {
        SetFromData();
        
        roomData.OnLocked += LockElement;
        roomData.OnUnlocked += UnlockElement;
    }

    private void OnDisable()
    {
        roomData.OnLocked -= LockElement;
        roomData.OnUnlocked -= UnlockElement;
        
        if (_manager && _manager.IsCurrentElement(this))
        {
            _manager.DeselectElement(this);
        }
    }
    
    public void SetFromData()
    {
        SetShopText(roomData.roomName, roomData.price.ToString(), roomData.width.ToString());
        SetShopSprite(roomData.roomIcon, roomData.roomType == RoomType.Guestroom ? Color.cyan : Color.red);
        
        if (roomData.isLocked)
        {
            LockElement();
        }
        else
        {
            UnlockElement();
        }
    }
    
    public void SetShopText(string itemName = null, string price = null, string cellSize = null)
    {
        MyUtility.SetText(nameText, itemName);
        MyUtility.SetText(priceText, price);
        MyUtility.SetText(cellText, "Cells: " + cellSize);
    }

    public void SetShopSprite(Sprite image, Color color)
    {
        iconImage.sprite = image;
        iconBackground.color = color;
    }

    public void SetShopBackground(Color color)
    {
        background.color = color;
    }
    
    public void LockElement()
    {
        if (_manager && _manager.IsCurrentElement(this))
        {
            _manager.DeselectElement(this);
        }
        
        isElementLocked = true;
        lockElement.SetActive(true);
    }
    
    public void UnlockElement()
    {
        isElementLocked = false;
        lockElement.SetActive(false);
    }
    
    public void FlipFlopLockState()
    {
        if (isElementLocked)
        {
            UnlockElement();
        }
        else
        {
            LockElement();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isElementLocked) { return; }

        _manager.OnElementClick(this);
    }

 

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (roomData != null)
        {
            SetFromData();
        }
    }
#endif
}