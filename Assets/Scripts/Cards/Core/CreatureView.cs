using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CreatureView: CardView
{
    Creature creature;
    TextMeshPro name_text, power_text, hp_text;
    public void init()
    {
        creature = GetComponent<Creature>();

        name_text = transform.Find("Name").Find("Text").GetComponent<TextMeshPro>();
        power_text = transform.Find("Power").Find("Text").GetComponent<TextMeshPro>();
        hp_text = transform.Find("Hp").Find("Text").GetComponent<TextMeshPro>();

        name_text.text = creature.creature_info.card_name;
        power_text.text = creature.creature_info.power.ToString();
        hp_text.text = creature.creature_info.hp.ToString();
    }

    public void on_damaged((int amount, Card source) value)
    {
        hp_text.text = creature.current_hp.ToString();
    }
}