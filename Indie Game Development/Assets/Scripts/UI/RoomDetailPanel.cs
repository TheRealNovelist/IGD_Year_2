using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetailPanel : MonoBehaviour
{
    public bool isPanelActive = false;

    [Header("Panel Elements")]
    [SerializeField] public GameObject returnButton;
    [SerializeField] public GameObject mainPanel;

    public void Start()
    {
        
    }

    public void OpenPanel()
    {
        if (!isPanelActive)
        {
            isPanelActive = true;
        }
    }

    public void ClosePanel()
    {
        if (isPanelActive)
        {
            isPanelActive = false;
        }
    }
}
