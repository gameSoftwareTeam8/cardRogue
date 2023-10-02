using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public interface ICardPool
{
    public List<CardInfo> get_list();
    public CardInfo sample();
    public List<CardInfo> sample_without_replacement(int number);
}

public class CardPool: ICardPool
{
    private string CREATURES_DIRECTORY = "Infos\\Creatures";
    private string MAGICS_DIRECTORY = "Infos\\Magics";
    List<CardInfo> infos = new();
    public CardPool()
    {
        init();
    }

    private void init()
    {
        infos.AddRange(Resources.LoadAll<CreatureInfo>(CREATURES_DIRECTORY));
        infos.AddRange(Resources.LoadAll<MagicInfo>(MAGICS_DIRECTORY));
    }
    
    public List<CardInfo> get_list()
    {
        return infos;
    }

    public CardInfo sample()
    {
        return infos[UnityEngine.Random.Range(0, infos.Count)];
    }

    public List<CardInfo> sample_without_replacement(int number)
    {
        var candidates = Enumerable.Range(0, infos.Count).ToList();
        List<CardInfo> result = new();
        for (int i = 0; i < number; i++)
        {
            int r = UnityEngine.Random.Range(0, candidates.Count);
            result.Add(infos[candidates[r]]);
            candidates.RemoveAt(r);
        }
        return result;
    }
}