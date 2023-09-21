using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Magic: Card
{
    public MagicInfo info;
    public void init(MagicInfo info)
    {
        this.info = Instantiate(info);
    }
}