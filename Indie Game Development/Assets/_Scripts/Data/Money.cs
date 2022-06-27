using System.Collections;
using System.Collections.Generic;
using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data/Money", fileName = "New Money Asset")]
public class Money : ScriptableObject
{
    public event Action OnMoneyChanged;

    [SerializeField] [ReadOnly]
    private int _money;

    public Money(int money)
    {
        _money = money;
    }

    public bool PayMoney(int amount)
    {
        if (amount > _money)
        {
            return false;
        }

        AddMoney(-amount);
        OnMoneyChanged?.Invoke();
        return true;
    }

    public void AddMoney(int amount)
    {
        _money += amount;
        OnMoneyChanged?.Invoke();
    }

    public int GetCurrentMoney()
    {
        return _money;
    }
}
