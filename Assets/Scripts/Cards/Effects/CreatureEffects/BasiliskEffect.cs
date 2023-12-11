using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasiliskEffect: CreatureEffect
{
    public override void on_turn_ended()
    {
        IBoard board = Locator.board;
        BoardSide target_side = board.get_opposite_side(creature);
        VFXFactory vfx_factory = Locator.vfx_factory;
        var vfx_object = vfx_factory.create("Shockwave");
        vfx_object.transform.position = new Vector3(0.0f, 0.0f, -50.0f);
        vfx_object.transform.localScale = new Vector3(58.0f, 58.0f, 1.0f);

        for (int i = 0; i < board.size; i++)
        {
            Creature target = board.get_card(target_side, i);
            if (target == null)
                continue;

            target?.take_damage(1, creature);
        }
    }
}