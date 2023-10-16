using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class CreatureVfx: CardVfx
{
    [SerializeField]
    private AnimationClip damage_animation = null;
    [SerializeField]
    private Material shattering_material = null;
    private GameObject hp_object;
    new void Awake()
    {
        base.Awake();
        Transform front = transform.Find("Front");
        hp_object = front.Find("Hp").Find("Text").gameObject;
    }

    public void on_damaged((int amount, Card source) info)
    {
        GameObject vfx_object = Instantiate(hp_object, hp_object.transform.parent);
        vfx_object.transform.SetParent(hp_object.transform, true);
        vfx_object.GetComponent<TextMeshPro>().text = info.amount.ToString();
        vfx_object.AddComponent<Animation>();
        vfx_object.AddComponent<Destroyer>();
        
        Animation animation = vfx_object.GetComponent<Animation>();
        animation.AddClip(damage_animation, "DamageAnimation");
        animation.Play("DamageAnimation");
    }

    public override void on_destroyed()
    {
        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            if (renderer.sprite == null)
                continue;
            
            renderer.material = shattering_material;
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
    }
}