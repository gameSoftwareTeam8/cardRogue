using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager: MonoBehaviour
{
    public CardInfo test_card_info;
    void Start()
    {
        Locator.card_factory.create(test_card_info);
    }
}