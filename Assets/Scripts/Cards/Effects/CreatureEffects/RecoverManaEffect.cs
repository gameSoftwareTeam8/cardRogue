using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RecoverManaEffect: CreatureEffect
{
    public override void on_created()
    {
        Locator.player.recover_mana(1);
    }
}