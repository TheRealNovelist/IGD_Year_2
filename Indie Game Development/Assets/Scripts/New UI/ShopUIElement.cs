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
    [SerializeField] private GameObject prefab;
    
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
    [HideInInspector] public bool isElementLocked;

    public void Init(ShopUIManager manager)
    {
        _manager = manager;
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
    
    public void SetLockState(bool isLocked)
    {
        if (isLocked && _manager.IsCurrentElement(this))
        {
            _manager.DeselectElement(this);
        }
        
        isElementLocked = isLocked;
        lockElement.SetActive(isLocked);
    }

    public void FlipFlopLockState()
    {
        SetLockState(!isElementLocked);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isElementLocked) { return; }

        _manager.OnElementClick(this);
    }
}