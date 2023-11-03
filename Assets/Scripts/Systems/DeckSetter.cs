using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class DeckSetter: MonoBehaviour
{
    public List<CardInfo> infos = new();
    void Awake()
    {
        IPlayer player = Locator.player;
        CardFactory factory = Locator.card_factory;
        foreach (var info in infos)
            player.add_card(factory.create(info).GetComponent<Card>());
    }
}