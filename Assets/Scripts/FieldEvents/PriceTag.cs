using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PriceTag: MonoBehaviour
{
    public int price
    {
        get {
            return _price;
        }
        set {
            _price = value;
            text.text = _price.ToString();
        }
    }
    private int _price;
    private TextMeshPro text;

    void Awake()
    {
        _price = 0;
        text = transform.Find("Text").GetComponent<TextMeshPro>();
    }
}