using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IMerchant
{
    public int cards_size { get; }
    public int cost_to_remove { get; }
    public Card get_card(int idx);
    public bool buy(int idx);
    public bool buy(Card card);
    public bool remove_card(int idx);
    public bool remove_card(Card card);
}

public class Merchant: IMerchant
{
    private const int CARDS_NUM = 3;
    private const int COST_TO_REMOVE = 75;
    private const int COST_TO_REMOVE_ADDEND = 25;

    private List<Card> cards = new();
    public int cards_size { get { return cards.Count; } }
    public int cost_to_remove { get; private set; }

    public Merchant()
    {
        cost_to_remove = COST_TO_REMOVE;

        var card_factory = Locator.card_factory;
        var sampled = Locator.card_pool.sample_without_replacement(CARDS_NUM);
        foreach (CardInfo info in sampled)
        {
            cards.Add(card_factory.create(info).GetComponent<Card>());
        }
    }

    public Card get_card(int idx)
    {
        return cards[idx];
    }

    public bool buy(int idx)
    {
        IPlayer player = Locator.player;
        player.balance = 1000; // FOR TEST
        Card card = get_card(idx);
        if (player.balance >= card.info.price) {
            player.balance -= card.info.price;
            player.add_card(card);
            return true;
        }
        return false;
    }

    public bool buy(Card card)
    {
        return buy(cards.IndexOf(card));;
    }

    public bool remove_card(int idx)
    {
        IPlayer player = Locator.player;
        if (player.balance >= cost_to_remove) {
            Card card = get_card(idx);
            player.remove_card(card);
            GameObject.Destroy(card.gameObject);
            cost_to_remove += COST_TO_REMOVE_ADDEND;
            return true;
        }
        return false;
    }

    public bool remove_card(Card card)
    {
        return remove_card(cards.IndexOf(card));
    }
}