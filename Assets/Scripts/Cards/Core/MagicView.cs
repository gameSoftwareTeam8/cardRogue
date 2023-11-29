using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MagicView: CardView
{
    Magic magic;
    TextMeshPro name_text, mana_text, description_text;
    SpriteRenderer magic_sprite;
    public void init()
    {
        magic = GetComponent<Magic>();
        Transform front = transform.Find("Front");
        name_text = front.Find("Name").Find("Text").GetComponent<TextMeshPro>();
        mana_text = front.Find("Mana").Find("Text").GetComponent<TextMeshPro>();
        magic_sprite = front.Find("Magic").GetComponent<SpriteRenderer>();

        name_text.text = magic.magic_info.card_name;
        mana_text.text = magic.magic_info.cost.ToString();
        description_text = front.Find("Description").Find("Text").GetComponent<TextMeshPro>();
        description_text.text = magic.magic_info.description;
        magic_sprite.sprite = magic.magic_info.sprite;
    }
}