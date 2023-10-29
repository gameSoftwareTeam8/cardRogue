using System;
using System.Collections;
using System.Collections.Generic;


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

            target.creature_info.hp += 1;
            target.creature_info.power += 1;
            vfx_factory.create("Buff").transform.position = target.transform.position;
        }
    }
}