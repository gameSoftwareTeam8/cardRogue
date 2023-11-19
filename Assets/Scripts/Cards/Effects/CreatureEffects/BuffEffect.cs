using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuffEffect: CreatureEffect
{
    public override void on_created()
    {
        IBoard board = Locator.board;
        VFXFactory vfx_factory = Locator.vfx_factory;
        BoardSide target_side = board.get_side(creature);
        for (int i = 0; i < board.size; i++)
        {
            Creature target = board.get_card(target_side, i);
            if (target == null || target == creature)
                continue;

            target.set_max_hp(target.creature_info.hp + 1, creature);
            target.set_power(target.creature_info.power + 1, creature);
            vfx_factory.create("Buff").transform.position = target.transform.position;
        }
    }
}