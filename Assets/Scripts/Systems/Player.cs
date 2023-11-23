using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public interface IPlayer
{
    public int max_hp { get; }
    public int hp { get; }
    public int max_mana { get; }
    public int mana { get; }
    public int balance { get; set; }
    public int cards_count { get; }
    public void take_damage(int amount);
    public void heal(int amount);
    public void pay_mana(int amount);
    public void recover_mana(int amount);
    public void draw(int num=1);
    public void add_card(Card card);
    public void remove_card(Card card);
    public Card get_card(int idx);
}

public class Player: IPlayer
{
    public int max_hp { get; private set; } = 20;
    public int hp { get; private set; } = 20;
    public int max_mana { get; private set; } = 3;
    public int mana { get; private set; }
    public int balance { get; set; }
    public int cards_count { get { return cards.Count; }}

    private List<Card> cards = new();
    public Card get_card(int idx)
    {
        GameObject card = GameObject.Instantiate(cards[idx].gameObject);
        GameObject.Destroy(card.GetComponent<DontDestroyer>());
        return card.GetComponent<Card>();
    }

    public void take_damage(int amount)
    {
        IEventManager event_manager = Locator.event_manager;
        hp -= amount;
        event_manager.notify("on_player_hp_changed");
    }

    public void heal(int amount)
    {
        IEventManager event_manager = Locator.event_manager;
        hp = Mathf.Min(max_hp, hp + amount);
        event_manager.notify("on_player_hp_changed");
    }
    
    public void pay_mana(int amount)
    {
        IEventManager event_manager = Locator.event_manager;
        mana = Mathf.Max(0, mana - amount);
        event_manager.notify("on_mana_changed");
    }

    public void recover_mana(int amount)
    {
        IEventManager event_manager = Locator.event_manager;
        mana = Mathf.Min(max_mana, mana + amount);
        event_manager.notify("on_mana_changed");
    }

    public void draw(int num=1)
    {
        for (int i = 0; i < num; i++)
            HandsManager.Inst.AddCard(TurnManager.Inst.myTurn);
    }

    public void add_card(Card card)
    {
        card.AddComponent<DontDestroyer>();
        cards.Add(card);
    }

    public void remove_card(Card card)
    {
        cards.Remove(card);
    }
}