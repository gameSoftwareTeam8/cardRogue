using System;
using System.Collections;
using System.Collections.Generic;


public class BasicCreatureEffect: CreatureEffect
{
    public override void on_created()
    {
        creature.take_damage(1, creature);
    }
}