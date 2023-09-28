using System;
using System.Collections;
using System.Collections.Generic;


public class BuffOnBattleEffect: CreatureEffect
{
    public override void on_battle_started()
    {
        creature.info.power += 1;
        creature.info.hp += 1;
    }
}