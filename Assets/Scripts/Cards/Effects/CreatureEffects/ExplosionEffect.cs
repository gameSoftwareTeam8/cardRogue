using System;
using System.Collections;
using System.Collections.Generic;


public class ExplosionEffect: CreatureEffect
{
    public override void on_destroyed()
    {
        IBoard board = Locator.board;
        BoardSide target_side = board.get_opposite_side(creature);
        for (int i = 0; i < board.size; i++)
        {
            Creature target = board.get_card(target_side, i);
            target?.take_damage(1, creature);
        }
    }
}