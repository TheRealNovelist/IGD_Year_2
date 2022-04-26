using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MoneyManager
{
    public static event Action OnMoneyChanged;

    private int money;

    public MoneyManager(int money)
    {
        this.money = money;
    }

    public bool PayMoney(int amount)
    {
        if (amount > money)
        {
            return false;
        }

        AddMoney(-amount);
        OnMoneyChanged?.Invoke();
        return true;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        OnMoneyChanged?.Invoke();
    }

    public int GetCurrentMoney()
    {
        return money;
    }
}
