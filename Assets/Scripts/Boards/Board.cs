using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BoardSide
{
    HOME = 0,
    AWAY = 1
}

/*
<callbacks>
on_card_added(card)
on_card_removed(card)
*/
public interface IBoard
{
    public int size { get; }
    public void init();
    public void run();
    public Creature get_card(BoardSide side, int idx);
    public void add_card(BoardSide side, int idx, Creature card);
    public void remove_card(BoardSide side, int idx);
    public void remove_card(Creature card);
    public BoardSide get_side(Creature card);
    public int get_idx(Creature card);
    public BoardSide get_opposite_side(Creature card);
    public Creature get_opposite_card(Creature card);
}

public class Board: MonoBehaviour, IBoard
{
    [SerializeField]
    private int _size = 3;
    public int size { get; private set; }

    private Creature[,] cards;
    public void init()
    {
        size = _size;
        cards = new Creature[2, size];
    }

    public void run()
    {

    }

    /// <summary> return null when out of bound </summary>
    public Creature get_card(BoardSide side, int idx)
    {
        if (idx < 0 || idx >= size)
            return null;
        return cards[(int)side, idx];
    }

    public void add_card(BoardSide side, int idx, Creature card)
    {
        if (cards[(int)side, idx] == null) {
            cards[(int)side, idx] = card;
            SendMessage("on_card_added", card, SendMessageOptions.DontRequireReceiver);
            card.GetComponent<CreatureEffect>()?.on_created();
        }
    }

    public void remove_card(BoardSide side, int idx)
    {
        Creature card = cards[(int)side, idx];
        if (card != null) {
            cards[(int)side, idx] = null;
            SendMessage("on_card_removed", card, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void remove_card(Creature card)
    {
        remove_card(get_side(card), get_idx(card));
    }

    public BoardSide get_side(Creature card)
    {
        for (int side = 0; side < 2; side++)
            for (int idx = 0; idx < size; idx++)
                if (cards[side, idx] == card)
                    return (BoardSide)side;
        throw null;
    }

    public int get_idx(Creature card)
    {
        for (int side = 0; side < 2; side++)
            for (int idx = 0; idx < size; idx++)
                if (cards[side, idx] == card)
                    return idx;
        throw null;
    }
    
    public BoardSide get_opposite_side(Creature card)
    {
        return (BoardSide)(1 - (int)get_side(card));
    }

    public Creature get_opposite_card(Creature card)
    {
        return cards[(int)get_opposite_side(card), get_idx(card)];
    }
}