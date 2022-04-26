using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        if (!gameManager)
            gameManager = GameObject.Find("_GameManager").GetComponent<GameManager>();
    }

    public void ChangeMoneyText()
    {
        amountText.text = gameManager.MoneyManager.GetCurrentMoney().ToString();
    }

    public void OnEnable()
    {
        MoneyManager.OnMoneyChanged += ChangeMoneyText;
        ChangeMoneyText();
    }

    public void OnDisable()
    {
        MoneyManager.OnMoneyChanged -= ChangeMoneyText;
    }
}
