using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class MagicEffect
{
    public Board board;
    public MagicEffect(Board board)
    {
        this.board = board;
    }

    /// <summary> 사용시 </summary> 
    public virtual void on_used() { }
}