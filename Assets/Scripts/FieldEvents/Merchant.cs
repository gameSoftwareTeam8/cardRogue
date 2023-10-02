using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IMerchant
{
    public int cards_size { get; }
    public int cost_to_remove { get; }
    public Card get_card(int idx);
    public bool buy(int idx);
    public bool remove_card(int idx);
}

public class Merchant: IMerchant
{
    private const int CARDS_NUM = 6;
    private const int COST_TO_REMOVE = 75;
    private const int COST_TO_REMOVE_ADDEND = 25;

    private List<Card> cards;
    public int cards_size { get { return cards.Count; } }
    public int cost_to_remove { get; private set; }

    public Merchant()
    {
        cost_to_remove = COST_TO_REMOVE;

        var card_factory = Locator.card_factory;
        var sampled = Locator.card_pool.sample(CARDS_NUM);
        foreach (CardInfo info in sampled)
            cards.Add(card_factory.create(info).GetComponent<Card>());
    }

    public Card get_card(int idx)
    {
        return cards[idx];
    }

    public bool buy(int idx)
    {
        IPlayer player = Locator.player;
        Card card = get_card(idx);
        if (player.balance >= card.info.cost) {
            player.balance -= card.info.cost;
            player.add_card(card.gameObject);
            return true;
        }
        return false;
    }

    public bool remove_card(int idx)
    {
        IPlayer player = Locator.player;
        if (player.balance >= cost_to_remove) {
            player.remove_card(idx);
            cost_to_remove += COST_TO_REMOVE_ADDEND;
            return true;
        }
        return false;
    }
}