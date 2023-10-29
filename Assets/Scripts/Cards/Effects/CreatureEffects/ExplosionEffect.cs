using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExplosionEffect: CreatureEffect
{
    public override void on_destroyed()
    {
        IBoard board = Locator.board;
        BoardSide target_side = board.get_opposite_side(creature);
        VFXFactory vfx_factory = Locator.vfx_factory;
        for (int i = 0; i < board.size; i++)
        {
            Creature target = board.get_card(target_side, i);
            if (target == null)
                continue;

            vfx_factory.create("Burst").transform.position = target.transform.position;
            target?.take_damage(1, creature);
        }
    }
}