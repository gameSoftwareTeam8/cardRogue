using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PortalEffect: MagicEffect
{
    public override void on_used(BoardSide user_side)
    {
        IBoard board = Locator.board;
        VFXFactory vfx_factory = Locator.vfx_factory;
        CardFactory factory = Locator.card_factory;
        for (int i = 0; i < board.size; i++)
        {
            if (board.get_card(user_side, i) != null)
                continue;
            
            Creature creature = factory.create("Creatures/Scourge").GetComponent<Creature>();
            board.add_card(user_side, i, creature);
            var vfx = vfx_factory.create("Summon");
            vfx.transform.position = creature.transform.position;
            vfx.transform.localScale *= 3.0f;
        }
    }
}