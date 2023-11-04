using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Magic: Card
{
    public MagicInfo magic_info;
    public void init(MagicInfo info)
    {
        magic_info = Instantiate(info);
        this.info = magic_info;
    }
}