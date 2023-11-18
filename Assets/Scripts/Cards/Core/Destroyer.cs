using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class Destroyer: MonoBehaviour
{
    public void destroy()
    {
        Destroy(gameObject);
    }
}

public class DestroyerWithParent: Destroyer
{
    public new void destroy()
    {
        base.destroy();
        Destroy(transform.parent.gameObject);
    }
}