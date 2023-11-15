using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeltingEffect: CreatureEffect
{
    public override void on_turn_started()
    {
        creature.take_damage(1, creature);
    }
}