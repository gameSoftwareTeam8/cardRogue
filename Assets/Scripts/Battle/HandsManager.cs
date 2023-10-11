using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class HandsManager : MonoBehaviour
{
    public static HandsManager Inst {  get; private set; }
    void Awake() => Inst = this;

    [SerializeField] List<Card> cards;

    public void AddCard(bool myTurn)
    {
        if(myTurn) {
            Card card = BattleDeck.Inst.DrawCard();
            cards.Add(card);
            SendMessage("OnCardAdded", (myTurn, card), SendMessageOptions.DontRequireReceiver);
        }
    }
}
