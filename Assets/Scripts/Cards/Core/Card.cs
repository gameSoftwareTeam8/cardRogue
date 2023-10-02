using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
<callbacks>
on_destroyed()
*/
public abstract class Card: MonoBehaviour
{
    public CardInfo info { get; set; }
    public virtual void destroy()
    {
        SendMessage("on_destroyed", SendMessageOptions.DontRequireReceiver);
    }
}