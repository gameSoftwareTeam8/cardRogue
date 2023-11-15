using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuffOnBattleEffect: CreatureEffect
{
    public override void on_battle_started()
    {
        creature.creature_info.power += 1;
        creature.creature_info.hp += 1;
    }
}