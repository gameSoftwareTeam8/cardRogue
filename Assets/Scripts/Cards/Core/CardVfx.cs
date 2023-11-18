using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class CardVfx: MonoBehaviour
{
    [SerializeField]
    protected Material burning_material;
    [SerializeField]
    protected AnimationClip destroy_animation;
    protected bool is_running_destroyed = false;
    private float time_to_destroy = 3.0f;
    private Card card;
    private float age = 0.001f;

    public void Awake()
    {
        card = GetComponent<Card>();
    }

    void FixedUpdate()
    {
        if (is_running_destroyed) {
            age += Time.deltaTime;
            foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
                renderer.material.SetFloat("_Age", age);
            if (age >= time_to_destroy)
                Destroy(gameObject);
        }
    }

    public virtual void on_destroyed()
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

        foreach (var text in GetComponentsInChildren<TextMeshPro>())
        {
            if (text.name != "Text")
                continue;

            text.AddComponent<Destroyer>();
            text.AddComponent<Animation>();
            Animation animation = text.GetComponent<Animation>();
            animation.AddClip(destroy_animation, "DamageAnimation");
            animation.Play("DamageAnimation");
        }
        is_running_destroyed = true;
    }
}