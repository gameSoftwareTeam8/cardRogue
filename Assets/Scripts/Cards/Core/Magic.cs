using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Magic: Card
{
    public new MagicInfo info { get; set; }
    public void init(MagicInfo info)
    {
        this.info = Instantiate(info);
    }
}