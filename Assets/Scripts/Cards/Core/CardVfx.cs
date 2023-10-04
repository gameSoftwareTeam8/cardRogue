using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class CardVfx: MonoBehaviour
{
    [SerializeField]
    private Material burning_material;
    private float time_to_destroy = 2.0f;
    private Card card;
    private float age = 0.0f;

    public void Awake()
    {
        card = GetComponent<Card>();
    }

    void FixedUpdate()
    {
        if (card.is_destroyed) {
            age += Time.deltaTime;
            foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
                renderer.material.SetFloat("_Age", age);
            if (age >= time_to_destroy)
                Destroy(gameObject);
        }
    }
    public void on_destroyed()
    {
        GameObject smoke_object = Locator.vfx_factory.create("Smoke");
        smoke_object.transform.parent = transform;
        smoke_object.transform.localPosition = Vector3.zero;
        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            if (renderer.sprite == null)
                continue;
            
            renderer.material = burning_material;
        }
    }
}