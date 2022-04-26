using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JanitorUI : MonoBehaviour
{
    private GameManager gameManager => FindObjectOfType<GameManager>();

    [SerializeField] private GameObject panel;

    [SerializeField] private TextMeshProUGUI maximumText;
    [SerializeField] private TextMeshProUGUI availableText;

    public void OpenPanel()
    {
        if (!BuildUI.IsBuildMenuActive())
        {
            gameManager.DismissAllPanel();

            panel.SetActive(true);
        }
    }

    private void Update()
    {
        maximumText.text = Room_Janitor.GetMaxJanitorAmount().ToString();
        availableText.text = Room_Janitor.GetAvailableJanitor().ToString();
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
