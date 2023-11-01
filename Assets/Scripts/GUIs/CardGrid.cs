using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGrid : MonoBehaviour
{
    public Player player;
    public GameObject cardPrefab;
    public Transform gridTransform;

    void Start()
    {
        DisplayCards();
    }

    void DisplayCards()
    {
        for (int i = 0; i < player.cards_count; i++)
        {
            Debug.Log(i);

            Card card = player.get_card(i);
            GameObject cardObj = Instantiate(cardPrefab, gridTransform);
            CardDisplay cardDisplay = cardObj.GetComponent<CardDisplay>();
            cardDisplay.DisplayCard(card);
        }
    }
}





