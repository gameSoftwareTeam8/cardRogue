using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CreatureView: CardView
{
    Creature creature;
    TextMeshPro name_text, mana_text, power_text, hp_text;
    public void init()
    {
        creature = GetComponent<Creature>();
        Transform front = transform.Find("Front");
        name_text = front.Find("Name").Find("Text").GetComponent<TextMeshPro>();
        mana_text = front.Find("Mana").Find("Text").GetComponent<TextMeshPro>();
        power_text = front.Find("Power").Find("Text").GetComponent<TextMeshPro>();
        hp_text = front.Find("Hp").Find("Text").GetComponent<TextMeshPro>();

        name_text.text = creature.creature_info.card_name;
        mana_text.text = creature.creature_info.cost.ToString();
        power_text.text = creature.creature_info.power.ToString();
        hp_text.text = creature.creature_info.hp.ToString();
    }

    public void on_damaged((int amount, Card source) value)
    {
        hp_text.text = creature.current_hp.ToString();
    }
}