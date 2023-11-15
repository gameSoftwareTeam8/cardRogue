using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpritDeerEffect: CreatureEffect
{
    public override void on_damaged((int amount, Card source) value)
    {
        IPlayer player = Locator.player;
        creature.heal(value.amount, creature);
        player.take_damage(value.amount);
    }
}