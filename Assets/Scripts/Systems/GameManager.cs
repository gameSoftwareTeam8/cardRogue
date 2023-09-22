using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager: MonoBehaviour
{
    public Player player;
    public List<CardInfo> test_card_info;
    void Awake()
    {
        Locator.player = player;

        foreach (var info in test_card_info)
        {
            GameObject card_object = Locator.card_factory.create(info);
            Locator.player.add_card(card_object);
        }
    }
}