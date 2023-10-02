using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public abstract class CardView: MonoBehaviour
{
    public event EventHandler on_clicked;
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

    void OnMouseDown()
    {
        on_clicked?.Invoke(this, EventArgs.Empty);
    }
}