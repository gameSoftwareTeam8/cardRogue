using System;
using System.Collections;
using System.Collections.Generic;


public class DrawOnCreatedEffect: CreatureEffect
{
    public override void on_created()
    {
        Locator.player.draw();
    }
}