using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public abstract class CardView: MonoBehaviour
{
    void Awake()
    {
        hide();
    }

    public void show()
    {
        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
            renderer.enabled = true;
        foreach (var tmp in GetComponentsInChildren<TextMeshPro>())
            tmp.enabled = true;
    }

    public void hide()
    {
        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
            renderer.enabled = false;
        foreach (var tmp in GetComponentsInChildren<TextMeshPro>())
            tmp.enabled = false;
    }
}