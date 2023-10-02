using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class MagicEffect: CardEffect
{
    protected Magic magic;
    public override void init()
    {
        magic = GetComponent<Magic>();
    }

    /// <summary> 사용시 </summary> 
    public virtual void on_used() { }
}

public abstract class TargetingMagicEffect: MagicEffect
{
    public virtual void on_used_to_target(Creature target) { }
}