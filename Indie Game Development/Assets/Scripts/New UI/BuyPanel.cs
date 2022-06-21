using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public class BuyPanel : MonoBehaviour
{
    [Title("Components")]
    [SerializeField] private Button buildButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private RectTransform panel;

    [Title("Settings")] 
    [SerializeField] private float slideTime = 1f;
    
    private void Start()
    {
        buildButton.onClick.AddListener(OpenPanel);
        returnButton.onClick.AddListener(ClosePanel);
    }
    
    void ClosePanel()
    {
        panel.anchoredPosition = new Vector2(-340, panel.anchoredPosition.y);
        panel.DOAnchorPosX(340, slideTime).OnComplete(() =>
        {
            panel.gameObject.SetActive(false);
            buildButton.gameObject.SetActive(true);
        });
    }
    
    void OpenPanel()
    {
        panel.gameObject.SetActive(true);
        buildButton.gameObject.SetActive(false);
        panel.anchoredPosition = new Vector2(340, panel.anchoredPosition.y);
        panel.DOAnchorPosX(-340, slideTime);
    }
}
