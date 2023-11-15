using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WerewolfEffect: CreatureEffect
{
    Sprite transformed = null;
    void Awake()
    {
        transformed = Resources.Load<Sprite>("Images/Cards/werewolf.png");
    }

    private bool is_transformed = false;
    public override void on_turn_started()
    {
        var renderer = creature.GetComponent<CreatureView>().character_sprite;
        if (is_transformed) {
            renderer.sprite = creature.creature_info.sprite;
            creature.creature_info.power -= 2;
            creature.creature_info.hp -= 2;
            is_transformed = false;
        }
        else {
            renderer.sprite = transformed;
            creature.creature_info.power += 2;
            creature.set_max_hp(creature.creature_info.hp + 2, creature);
            is_transformed = true;
        }
    }
}