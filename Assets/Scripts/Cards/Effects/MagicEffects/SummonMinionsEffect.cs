using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SummonMinionsEffect: MagicEffect
{
    public override void on_used(BoardSide user_side)
    {
        IBoard board = Locator.board;
        CardFactory factory = Locator.card_factory;
        for (int i = 0; i < board.size; i++)
        {
            if (board.get_card(user_side, i) != null)
                continue;
            
            Creature creature = factory.create("Creatures/Minion").GetComponent<Creature>();
            board.add_card(user_side, i, creature);
        }
    }
}