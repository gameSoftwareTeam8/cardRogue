using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CreatureView: CardView
{
    TextMeshPro name_text, power_text, hp_text;
    public void init(CreatureInfo creature_info)
    {
        name_text = transform.Find("Name").Find("Text").GetComponent<TextMeshPro>();
        power_text = transform.Find("Power").Find("Text").GetComponent<TextMeshPro>();
        hp_text = transform.Find("Hp").Find("Text").GetComponent<TextMeshPro>();

        name_text.text = creature_info.card_name;
        power_text.text = creature_info.power.ToString();
        hp_text.text = creature_info.hp.ToString();
    }
}