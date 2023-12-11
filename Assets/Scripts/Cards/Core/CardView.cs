using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class CardEventArgs: EventArgs
{
    public Card card;
    public CardEventArgs(Card card): base()
    {
        this.card = card;
    }
}

public abstract class CardView: MonoBehaviour
{
    public event EventHandler<CardEventArgs> on_mouse_up, on_mouse_down, on_mouse_over, on_mouse_exit;
    public bool is_front { get; private set; } = false;
    private Card card;
    void Awake()
    {
        hide();
        card = GetComponent<Card>();
    }

    public void show()
    {
        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
            renderer.enabled = true;
        foreach (var tmp in GetComponentsInChildren<TextMeshPro>())
            tmp.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void hide(bool enable_collider=false)
    {
        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
            renderer.enabled = false;
        foreach (var tmp in GetComponentsInChildren<TextMeshPro>())
            tmp.enabled = false;
        GetComponent<BoxCollider2D>().enabled = enable_collider;
    }

    public void flip(bool is_front)
    {
        this.is_front = is_front;
        foreach (var tmp in GetComponentsInChildren<TextMeshPro>())
            tmp.enabled = is_front;
    }

    void OnMouseOver()
    {
        on_mouse_over?.Invoke(this, new CardEventArgs(card));
    }

    void OnMouseExit()
    {
        on_mouse_exit?.Invoke(this, new CardEventArgs(card));
    }

    void OnMouseUp()
    {
        on_mouse_up?.Invoke(this, new CardEventArgs(card));
    }

    void OnMouseDown()
    {
        on_mouse_down?.Invoke(this, new CardEventArgs(card));
    }
}