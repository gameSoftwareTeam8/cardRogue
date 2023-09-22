using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardFactory
{
    GameObject creature_prefab, magic_prefab;
    public CardFactory()
    {
        creature_prefab = Resources.Load<GameObject>("Prefabs/Cards/Creature");
        magic_prefab = Resources.Load<GameObject>("Prefabs/Cards/Magic");
    }
    
    public GameObject create(CardInfo card_info)
    {
        GameObject game_object = create_card_type(card_info);
        var effect_type = Type.GetType(card_info.effect_name);
        if (effect_type is not null)
            game_object.AddComponent(effect_type);
        game_object.name = card_info.card_name;
        
        return game_object;
    }

    public GameObject create(string card_name)
    {
        CardInfo info = Resources.Load<CardInfo>("Infos/" + card_name);
        return create(info);
    }

    private GameObject create_card_type(CardInfo card_info)
    {
        GameObject game_object = null;
        if (card_info is CreatureInfo creature_info) {
            game_object = GameObject.Instantiate(creature_prefab);
            game_object.GetComponent<Creature>().init(creature_info);
            game_object.GetComponent<CreatureView>().init(creature_info);
        }
        else if (card_info is MagicInfo magic_info) {
            game_object = GameObject.Instantiate(magic_prefab);
            game_object.GetComponent<Magic>().init(magic_info);
            // game_object.GetComponent<MagicView>().init(magic_info);
        }
        return game_object;
    }
}