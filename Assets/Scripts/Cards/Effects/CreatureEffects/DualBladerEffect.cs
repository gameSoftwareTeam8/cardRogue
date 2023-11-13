using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DualBladerEffect: CreatureEffect
{
    public override void on_battle_started()
    {
        IBoard board = Locator.board;
        Creature target = board.get_opposite_card(creature);
        if (target != null)
            creature.attack(target);
    }
}