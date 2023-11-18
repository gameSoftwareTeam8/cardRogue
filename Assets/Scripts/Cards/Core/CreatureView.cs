using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CreatureView: CardView
{
    Creature creature;
    TextMeshPro name_text, mana_text, power_text, hp_text, description_text;
    public SpriteRenderer character_sprite { get; private set; }
    public void init()
    {
        creature = GetComponent<Creature>();
        Transform front = transform.Find("Front");
        name_text = front.Find("Name").Find("Text").GetComponent<TextMeshPro>();
        mana_text = front.Find("Mana").Find("Text").GetComponent<TextMeshPro>();
        power_text = front.Find("Power").Find("Text").GetComponent<TextMeshPro>();
        hp_text = front.Find("Hp").Find("Text").GetComponent<TextMeshPro>();
        description_text = front.Find("Description").Find("Text").GetComponent<TextMeshPro>();
        character_sprite = front.Find("Character").GetComponent<SpriteRenderer>();

        name_text.text = creature.creature_info.card_name;
        mana_text.text = creature.creature_info.cost.ToString();
        power_text.text = creature.creature_info.power.ToString();
        hp_text.text = creature.creature_info.hp.ToString();
        description_text.text = creature.creature_info.description;
        character_sprite.sprite = creature.creature_info.sprite;
    }

    public void on_damaged((int amount, Card source) value)
    {
        hp_text.text = creature.current_hp.ToString();
    }
}