using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeteorEffect: MagicEffect
{
    public override void on_used(BoardSide user_side)
    {
        IBoard board = Locator.board;
        BoardSide side = (BoardSide)(1 - user_side);
        for (int idx = 0; idx < board.size; idx++)
            board.get_card(side, idx)?.take_damage(3, magic);
    }
}