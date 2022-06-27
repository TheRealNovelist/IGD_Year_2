using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class BuyPanel : MonoBehaviour
{
    [Title("Components")]
    [SerializeField] private RectTransform panel;
    [HorizontalGroup()]
    [SerializeField] private Button buildButton;
    [SerializeField] private Button returnButton;

    [Title("Settings")] 
    [SerializeField] private float slideTime = 1f;

    [Title("Events")] 
    public UnityEvent OnOpenPanel;
    public UnityEvent OnClosePanel;
    
    private void Start()
    {
        buildButton.onClick.AddListener(OpenPanel);
        returnButton.onClick.AddListener(ClosePanel);
    }

    void OpenPanel()
    {
        panel.gameObject.SetActive(true);
        buildButton.gameObject.SetActive(false);
        panel.anchoredPosition = new Vector2(340, panel.anchoredPosition.y);
        panel.DOAnchorPosX(-340, slideTime);
        OnOpenPanel.Invoke();
    }
    
    void ClosePanel()
    {
        panel.anchoredPosition = new Vector2(-340, panel.anchoredPosition.y);
        panel.DOAnchorPosX(340, slideTime).OnComplete(() =>
        {
            panel.gameObject.SetActive(false);
            buildButton.gameObject.SetActive(true);
        });
        OnClosePanel.Invoke();
    }
}
