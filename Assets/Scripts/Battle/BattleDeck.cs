using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleDeck : MonoBehaviour
{
    public static BattleDeck Inst {  get; private set; }
    [SerializeField] List<Card> cardBuffer = new();

    void Awake()
    {
        Inst = this;
        SetupCardBuffer();
    }

    public Card DrawCard()
    {
        if(cardBuffer.Count == 0)
            SetupCardBuffer();

        Card card = cardBuffer[0];
        cardBuffer.RemoveAt(0);
        return card;
    }
    
    void SetupCardBuffer()
    {
        IPlayer player = Locator.player;
        for (int i = 0; i < player.cards_count; i++)
        {
            var cardObject = Instantiate(player.get_card(i).gameObject);
            cardBuffer.Add(cardObject.GetComponent<Card>());
        }

        for(int i = 0; i < cardBuffer.Count; i++)
        {
            int rand = Random.Range(i, cardBuffer.Count);
            var temp = cardBuffer[i];
            cardBuffer[i] = cardBuffer[rand];
            cardBuffer[rand] = temp;    
        }
    }
}
