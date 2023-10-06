using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class CreatureVfx: CardVfx
{
    [SerializeField]
    private AnimationClip damage_animation = null;
    private GameObject hp_object;
    new void Awake()
    {
        base.Awake();
        hp_object = transform.Find("Hp").Find("Text").gameObject;
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
}