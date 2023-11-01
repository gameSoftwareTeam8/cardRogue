using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRenderingOrderer : MonoBehaviour
{
    [SerializeField] string sortingLayerName = "Card";
    int originOrder, lastOrder = 0;

    public void SetOriginOrder(int originOrder)
    {
        this.originOrder = originOrder;
        SetOrder(originOrder);
    }

    public void SetMostFrontOrder(bool isMostFront)
    {
        SetOrder(isMostFront ? 100 : originOrder);
    }
    
    public void SetOrder(int order)
    {
        int mulOrder = order * 10;
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder += mulOrder - lastOrder;
        }
        lastOrder = mulOrder;
    }
}
