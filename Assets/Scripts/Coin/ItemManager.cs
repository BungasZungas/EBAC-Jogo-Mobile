using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ZUNGAS.Core.Singleton;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    public SOInt coinsBlue;
    public TextMeshProUGUI uiTextCoins;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins.value = 0;
        coinsBlue.value = 0;
        UpdateUI();
    }

    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
        UpdateUI();
    }
    
    public void AddBlueCoins(int amount = 1)
    {
        coinsBlue.value += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        //uiTextCoins.text = "x " + coins.ToString();
        //UIInGameManager.UpdateTextCoins(coins.value.ToString());
    }
}
