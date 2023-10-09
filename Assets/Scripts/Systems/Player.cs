using System.Collections;
using System.Collections.Generic;
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
    public void add_card(GameObject card_object);
    public void remove_card(GameObject card_object);
    public GameObject get_card(int idx);
}

public class Player: IPlayer
{
    public int max_hp { get; private set; } = 50;
    public int hp { get; private set; }
    public int max_mana { get; private set; } = 3;
    public int mana { get; private set; }
    public int balance { get; set; }
    public int cards_count { get { return card_objects.Count; }}

    private List<GameObject> card_objects = new();
    void Awake()
    {
        hp = max_hp;
    }

    public GameObject get_card(int idx)
    {
        return card_objects[idx];
    }

    public void take_damage(int amount)
    {
        hp -= amount;
    }

    public void heal(int amount)
    {
        hp = Mathf.Min(max_hp, hp + amount);
    }
    
    public void pay_mana(int amount)
    {
        mana = Mathf.Max(0, mana - amount);
    }

    public void recover_mana(int amount)
    {
        mana = Mathf.Min(max_mana, mana + amount);
    }

    public void draw(int num=1)
    {

    }

    public void add_card(GameObject card_object)
    {
        card_objects.Add(card_object);
    }

    public void remove_card(GameObject card_object)
    {
        card_objects.Remove(card_object);
    }
}