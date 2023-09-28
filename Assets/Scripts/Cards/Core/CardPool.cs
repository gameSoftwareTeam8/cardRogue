using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;


public interface ICardPool
{
    public List<CardInfo> get_list();
    public CardInfo sample();
    public List<CardInfo> sample(int number);
}

public class CardPool: ICardPool
{
    public CardPool()
    {
        init();
    }

    private void init()
    {
        
    }
    
    public List<CardInfo> get_list()
    {
        throw new NotImplementedException();
    }

    public CardInfo sample()
    {
        throw new NotImplementedException();
    }

    public List<CardInfo> sample(int number)
    {
        throw new NotImplementedException();
    }
}