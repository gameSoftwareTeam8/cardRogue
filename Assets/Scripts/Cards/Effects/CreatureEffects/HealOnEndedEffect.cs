using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealOnEndedEffect: CreatureEffect
{
    public override void on_turn_ended()
    {
        IBoard board = Locator.board;
        VFXFactory vfx_factory = Locator.vfx_factory;
        BoardSide target_side = board.get_side(creature);
        for (int i = 0; i < board.size; i++)
        {
            Creature target = board.get_card(target_side, i);
            if (target == null)
                continue;
            creature.heal(2, creature);
            vfx_factory.create("Buff").transform.position = target.transform.position;
        }
    }
}