using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPlayer
{
    public int max_hp { get; }
    public int hp { get; }
    public int balance { get; set; }
    public int cards_count { get; }
    public void take_damage(int amount);
    public void heal(int amount);
    public void add_card(GameObject card_object);
    public void remove_card(int idx);
    public GameObject get_card(int idx);
}

public class Player: IPlayer
{
    private int _max_hp = 50;

    public int max_hp { get; private set; }
    public int hp { get; private set; }
    public int balance { get; set; }
    public int cards_count { get { return card_objects.Count; }}

    private List<GameObject> card_objects = new();
    void Awake()
    {
        max_hp = _max_hp;
        hp = max_hp;
    }

    public GameObject get_card(int idx)
    {
        return card_objects[idx];
    }

    public void add_card(GameObject card_object)
    {
        card_objects.Add(card_object);
    }

    public void remove_card(int idx)
    {
        card_objects.RemoveAt(idx);
    }

    public void take_damage(int amount)
    {
        hp -= amount;
    }

    public void heal(int amount)
    {
        hp = Mathf.Min(max_hp, hp + amount);
    }
}