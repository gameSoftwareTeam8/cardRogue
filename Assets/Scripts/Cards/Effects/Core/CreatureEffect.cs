using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


public abstract class CreatureEffect: CardEffect
{
    protected Creature creature;
    public override void init()
    {
        creature = GetComponent<Creature>();
    }

    /// <summary> 지속 효과 </summary> 
    public virtual void passive() { }

    /// <summary> 카드가 보드에 등장할 시 </summary> 
    public virtual void on_created() { }
    
    /// <summary> 카드가 파괴될 시 </summary> 
    public virtual void on_destroyed() { }

    /// <summary> 턴 시작시 </summary>
    public virtual void on_turn_started() { }

    /// <summary> 턴 종료시 </summary> 
    public virtual void on_turn_ended() { }
    
    /// <summary> 전투 전 </summary> 
    public virtual void on_battle_started() { }
    
    /// <summary> 전투 후 </summary> 
    public virtual void on_battle_ended() { }

    /// <summary> 피해를 입었을 때 </summary>
    public virtual void on_damaged((int amount, Card source) value) { }
    
    /// <summary> 회복했을 때 </summary>
    public virtual void on_healed((int amount, Card source) value) { }
}