using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    [TitleGroup("Colors")] 
    [SerializeField] private Color deselectedColor = Color.grey;
    [SerializeField] private Color selectedColor = Color.green;

    private List<ShopUIElement> _allElements;
    private ShopUIElement _currentElement;
    
    private void Awake()
    {
        _allElements = GetComponentsInChildren<ShopUIElement>(true).ToList();

        _currentElement = null;
        
        foreach (ShopUIElement element in _allElements)
        {
            element.Init(this);
        }
    }

    public bool IsCurrentElement(ShopUIElement element)
    {
        return _currentElement == element;
    }
    
    public void SelectElement(ShopUIElement element)
    {
        _currentElement = element;
        element.SetShopBackground(selectedColor);
    }
    
    public void DeselectElement(ShopUIElement element)
    {
        _currentElement = null;
        element.SetShopBackground(deselectedColor);
    }

    public void OnElementClick(ShopUIElement newElement)
    {
        if (_currentElement == newElement)
        {
            DeselectElement(_currentElement);
            return;
        }

        foreach (ShopUIElement element in _allElements)
        {
            DeselectElement(element);
        }
        
        SelectElement(newElement);
    }
}
