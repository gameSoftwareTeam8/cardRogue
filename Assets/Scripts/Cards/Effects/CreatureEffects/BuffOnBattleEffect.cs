using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuffOnBattleEffect: CreatureEffect
{
    public override void on_battle_started()
    {
        creature.set_max_hp(creature.creature_info.hp + 1, creature);
        creature.set_power(creature.creature_info.power + 1, creature);
    }
}