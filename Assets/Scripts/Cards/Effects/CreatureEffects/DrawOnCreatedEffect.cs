using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawOnCreatedEffect: CreatureEffect
{
    public override void on_created()
    {
        Locator.player.draw();
    }
}