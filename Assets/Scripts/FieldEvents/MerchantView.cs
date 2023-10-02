using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MerchantView: MonoBehaviour
{   
    public List<Transform> card_zones = new();
    private IMerchant merchant;
    void Awake()
    {
        merchant = new Merchant();
        for (int i = 0; i < merchant.cards_size; i++)
        {
            Card card = merchant.get_card(i);
            if (i < card_zones.Count) {
                card.transform.position = card_zones[i].position;
                CardView card_view = card.GetComponent<CardView>();
                card_view.show();
                card_view.on_clicked += on_card_cliked;
            }
        }
    }

    public void on_card_cliked(object sender, EventArgs args)
    {
        CardView card_view = (CardView)sender;
        Card card = card_view.GetComponent<Card>();
        if (merchant.buy(card)) {
            Debug.Log("Bought");
            merchant.remove_card(card);
        }
        else
            Debug.Log("Not enough money");
    }
}