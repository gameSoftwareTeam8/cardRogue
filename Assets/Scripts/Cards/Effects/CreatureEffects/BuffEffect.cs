using System;
using System.Collections;
using System.Collections.Generic;


public class BuffEffect: CreatureEffect
{
    public override void on_created()
    {
        IBoard board = Locator.board;
        BoardSide target_side = board.get_side(creature);
        for (int i = 0; i < board.size; i++)
        {
            Creature target = board.get_card(target_side, i);
            if (target == null || target == creature)
                continue;

            target.info.hp += 1;
            target.info.power += 1;
        }
    }
}