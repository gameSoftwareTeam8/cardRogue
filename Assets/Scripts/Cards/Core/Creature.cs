using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
<callbacks>
on_damaged((int amount, Card source))
on_healed((int amount, Card source))
*/
public class Creature: Card
{
    public CreatureInfo creature_info;
    public int current_hp { get; private set; }
    public void init(CreatureInfo info)
    {
        creature_info = Instantiate(info);
        this.info = creature_info;
        current_hp = info.hp;
    }
    
    public override void destroy()
    {
        if (is_destroyed)
            return;
        base.destroy();
        Locator.board.remove_card(this);
    }

    public void attack(Creature target)
    {
        SendMessage("on_battle_started", SendMessageOptions.DontRequireReceiver);
        target.take_damage(creature_info.power, this);
        SendMessage("on_battle_ended", SendMessageOptions.DontRequireReceiver);
    }

    public void take_damage(int amount, Card source)
    {
        if (is_destroyed)
            return;

        current_hp -= amount;
        SendMessage("on_damaged", (amount, source), SendMessageOptions.DontRequireReceiver);
        if (current_hp <= 0)
            destroy();
    }

    public void heal(int amount, Card source)
    {
        if (is_destroyed)
            return;
            
        current_hp = Mathf.Min(creature_info.hp, current_hp + amount);       
        SendMessage("on_healed", (amount, source), SendMessageOptions.DontRequireReceiver);
    }
}