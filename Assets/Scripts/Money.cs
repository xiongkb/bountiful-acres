using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Money : MonoBehaviour
{
    public static Money instance;
    public TMP_Text moneyDisplay;
    public int money = 0;

    void Awake()
    {
        instance = this;
        moneyDisplay.SetText("$" + money.ToString());
    }

    public int CalcMoney(int strawberries, int carrots, int potatoes) {
        return strawberries * 5 + carrots * 8 + potatoes * 3;
    }
    
    public void AddMoney(int strawberries, int carrots, int potatoes)
    {
        money += CalcMoney(strawberries, carrots, potatoes);
        moneyDisplay.SetText("$" + money.ToString());
    }

    public void SubtractMoney(int num) {
        money -= num;
        moneyDisplay.SetText("$" + money.ToString());
    }
}
