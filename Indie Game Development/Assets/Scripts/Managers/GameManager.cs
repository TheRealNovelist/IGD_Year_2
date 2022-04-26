using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MoneyManager MoneyManager { get; private set; }
    public ReputationManager ReputationManager { get; private set; }

    public GameObject[] UIPanels;

    private void Awake()
    {
        MoneyManager = new MoneyManager(20000);
        ReputationManager = new ReputationManager(0, 0);
    }

    public void DismissAllPanel()
    {
        foreach (GameObject panel in UIPanels)
        {
            panel.SetActive(false);
        }
    }
}
