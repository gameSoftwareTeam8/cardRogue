using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArcherEffect: CreatureEffect
{
    public override void on_created()
    {
        IBoard board = Locator.board;
        board.get_opposite_card(creature)?.take_damage(2, creature);
    }
}