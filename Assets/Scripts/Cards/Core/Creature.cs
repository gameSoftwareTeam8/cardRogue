using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Creature: Card
{
    public CreatureInfo info;
    private int current_hp;
    public Creature(CreatureInfo info)
    {
        this.info = info;
        current_hp = info.hp;
    }
    
    public override void destroy()
    {
        base.destroy();
    }

    public void take_damage(int amount, Card source)
    {
        current_hp -= amount;
        if (current_hp <= 0)
            destroy();
    }

    public void heal(int amount, Card source)
    {
        current_hp = Mathf.Min(info.hp, current_hp + amount);       
    }
}