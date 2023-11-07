using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    public Player player;

    public TextMeshProUGUI hpTxt, mpTxt, moneyTxt;


    void Update()
    {
        UpdatePlayerStatusUI();
    }
    void UpdatePlayerStatusUI()
    {
        IPlayer player = Locator.player;

        int playerHp = player.hp;
        int playerMaxHp = player.max_hp;
        int playerMana = player.max_mana;
        int playerMaxMana = player.max_mana;
        int playerBalance = player.balance;

        hpTxt.text = playerHp.ToString() + " / " + playerMaxHp.ToString();
        mpTxt.text = playerMaxMana.ToString() + " / " + playerMana.ToString();
        moneyTxt.text = playerBalance.ToString();

    }
}
