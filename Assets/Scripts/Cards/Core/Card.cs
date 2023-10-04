using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
<callbacks>
on_destroyed()
*/
public abstract class Card: MonoBehaviour
{
    public CardInfo info;
    public bool is_destroyed { get; private set; }
    public Card()
    {
        is_destroyed = false;    
    }

    public virtual void destroy()
    {
        if (is_destroyed)
            return;

        is_destroyed = true;
        SendMessage("on_destroyed", SendMessageOptions.DontRequireReceiver);
    }
}