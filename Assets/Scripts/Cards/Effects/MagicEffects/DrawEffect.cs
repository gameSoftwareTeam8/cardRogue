using System;
using System.Collections;
using System.Collections.Generic;


public class DrawEffect: MagicEffect
{
    public override void on_used()
    {
        Locator.player.draw(2);
    }
}