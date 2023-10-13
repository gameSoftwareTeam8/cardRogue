using System;
using System.Collections;
using System.Collections.Generic;


public class DrawEffect: MagicEffect
{
    public override void on_used(BoardSide user_side)
    {
        Locator.player.draw(2);
    }
}