using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class HpBar: MonoBehaviour
{
    private Material bar_material = null;
    private TextMeshPro hp_text = null;
    void Awake()
    {
        bar_material = transform.Find("Bar").GetComponent<SpriteRenderer>().material;
        hp_text = transform.Find("Text").GetComponent<TextMeshPro>();

        IEventManager event_manager = Locator.event_manager;
        event_manager.register("on_player_hp_changed", on_player_hp_changed);
        on_player_hp_changed();
    }

    public void on_player_hp_changed()
    {
        IPlayer player = Locator.player;
        bar_material.SetFloat("_Height", (float)player.hp / player.max_hp * 0.7f);
        hp_text.text = player.hp.ToString();
    }

    void OnDestroy()
    {
        IEventManager event_manager = Locator.event_manager;
        event_manager.remove("on_player_hp_changed", on_player_hp_changed);
    }
}
