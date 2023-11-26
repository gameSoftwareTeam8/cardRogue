using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    public int attack(Creature target)
    {
        SendMessage("on_battle_started", SendMessageOptions.DontRequireReceiver);
        int over_power = target.take_damage(creature_info.power, this);
        SendMessage("on_battle_ended", SendMessageOptions.DontRequireReceiver);
        return over_power;
    }

    public int take_damage(int amount, Card source)
    {
        if (is_destroyed)
            return 0;

        current_hp -= amount;
        SendMessage("on_damaged", (amount, source), SendMessageOptions.DontRequireReceiver);
        if (current_hp <= 0) {
            destroy();
            return -current_hp;
        }
        return 0;
    }

    public void heal(int amount, Card source)
    {
        if (is_destroyed)
            return;
            
        current_hp = Mathf.Min(creature_info.hp, current_hp + amount);       
        SendMessage("on_healed", (amount, source), SendMessageOptions.DontRequireReceiver);
    }

    public void set_max_hp(int value, Card source)
    {
        if (is_destroyed)
            return;

        creature_info.hp = value;
        current_hp += value;
        current_hp = Mathf.Min(current_hp, creature_info.hp);
        SendMessage("on_value_changed", SendMessageOptions.DontRequireReceiver);
    }

    public void set_power(int value, Card source)
    {
        if (is_destroyed)
            return;

        creature_info.power = value;
        SendMessage("on_value_changed", SendMessageOptions.DontRequireReceiver);
    }
}