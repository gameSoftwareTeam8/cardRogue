using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BoardView: MonoBehaviour
{
    public float width;

    private IBoard board;
    private Vector3[,] positions;
    public void init()
    {
        board = GetComponent<Board>();
        positions = new Vector3[2, board.size];
        for (int idx = 0; idx < board.size; idx++)
        {
            positions[0, idx] = transform.Find("HomeSpace" + idx).transform.localPosition;
            positions[1, idx] = transform.Find("AwaySpace" + idx).transform.localPosition;
        }
    }

    public void on_card_added(Creature card)
    {
        card.transform.parent = transform;
        card.transform.localPosition = positions[(int)board.get_side(card), board.get_idx(card)];
        card.transform.localScale = new Vector3(1.875f, 1.875f, 1.0f);
        card.GetComponent<CardView>().show();
    }
}