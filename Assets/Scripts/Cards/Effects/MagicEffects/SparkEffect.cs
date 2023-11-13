using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SparkEffect: TargetingMagicEffect
{
    public override void on_used_to_target(Creature target)
    {
        target.take_damage(3, magic);
    }
}