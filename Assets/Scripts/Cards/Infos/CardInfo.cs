using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum CardRarity
{
    COMMON = 0,
    UNCOMMON = 1,
    RARE = 2,
    EPIC = 3
}

public abstract class CardInfo : ScriptableObject
{
    public string card_name, description, effect_name;
    public CardRarity rarity;
    public int cost;
    public Sprite sprite;
}