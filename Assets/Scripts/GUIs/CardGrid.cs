using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGrid : MonoBehaviour
{
    public Transform gridTransform;

    void Start()
    {
        DisplayCards();
    }

    void DisplayCards()
    {
        IPlayer player = Locator.player;
        for (int i = 0; i < player.cards_count; i++)
        {
            Card card = player.get_card(i);
            GameObject cardObj = Instantiate(card.gameObject, gridTransform);
            cardObj.GetComponent<CardView>().show();
        }
    }
}





