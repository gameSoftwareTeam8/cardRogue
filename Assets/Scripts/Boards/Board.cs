using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BoardSide
{
    HOME = 0,
    AWAY = 1
}

public interface IBoard
{
    public int size { get; }
    public void init();
    public void run();
    public BoardSide get_side(Card card);
    public int get_idx(Card card);
    public Card get_card(BoardSide side, int idx);
    public Card get_opposite_card(Card card);
}

public class Board: MonoBehaviour, IBoard
{
    [SerializeField]
    private int _size = 3;
    public int size { get; private set; }

    private List<List<Card>> cards = new(2);

    public void init()
    {
        size = _size;
        cards[0] = new List<Card>(size);
        cards[1] = new List<Card>(size);
    }

    public void run()
    {

    }

    public Card get_card(BoardSide side, int idx)
    {
        return cards[(int)side][idx];
    }

    public BoardSide get_side(Card card)
    {
        for (int side = 0; side < 2; side++)
            for (int idx = 0; idx < size; idx++)
                if (cards[side][idx] == card)
                    return (BoardSide)side;
        throw null;
    }

    public int get_idx(Card card)
    {
        for (int side = 0; side < 2; side++)
            for (int idx = 0; idx < size; idx++)
                if (cards[side][idx] == card)
                    return idx;
        throw null;
    }

    public Card get_opposite_card(Card card)
    {
        return cards[1 - (int)get_side(card)][get_idx(card)];
    }
}