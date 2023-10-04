using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnManager
{
    public int turn { get; private set; }
    public void init()
    {
        turn = 0;
    }

    public void battle()
    {
        IBoard board = Locator.board;

        for (int i = 0; i < board.size; i++)
        {
            for (int side = 0; side < 2; side++)
            {
                Creature card = board.get_card((BoardSide)side, i);
                Creature target = board.get_opposite_card(card);
                if (card != null && target != null)
                    card.attack(target);
            }
        }

        turn++;
    }
}