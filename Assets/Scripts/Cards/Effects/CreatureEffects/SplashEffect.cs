using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SplashEffect: CreatureEffect
{
    public override void on_battle_ended()
    {
        IBoard board = Locator.board;
        BoardSide target_side = board.get_opposite_side(creature);
        int idx = board.get_idx(creature);
        foreach (int i in new int[]{-1, 1})
        {
            Creature target = board.get_card(target_side, idx - i);
            if (target != null)
                target.take_damage(creature.creature_info.power, creature);
        }
    }
}