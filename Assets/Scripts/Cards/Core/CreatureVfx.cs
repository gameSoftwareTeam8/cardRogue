using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.U2D;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Collections;


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
        var bounds = GetComponent<SpriteMask>().bounds;
        var origin = bounds.min.ConvertTo<Vector2>() - Vector2.one * 0.45f;
        var size = new Vector2(bounds.max.x - bounds.min.x, bounds.max.y - bounds.min.y) + Vector2.one * 0.45f;
        
        Vector2[] temp_default = {
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(0, 0),
            new Vector2(1, 0)
        };
        NativeArray<Vector2> default_uv = new(temp_default, Allocator.Temp);

        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            if (renderer.sprite == null)
                continue;
            
            renderer.material = shattering_material;
            var uv = renderer.sprite.uv;
            // renderer.material.SetTexture("_MainTex", renderer.sprite.texture);

            var min = renderer.bounds.min;
            var max = renderer.bounds.max;
            Vector2[] temp = {
                new Vector2(min.x, max.y),
                new Vector2(max.x, max.y),
                new Vector2(min.x, min.y),
                new Vector2(max.x, min.y)
            };
            for (int i = 0; i < 4; i++)
                temp[i] = (temp[i] - origin) / size;

            NativeArray<Vector2> shattering_uv = new(temp, Allocator.Temp);
            renderer.sprite.SetVertexAttribute(VertexAttribute.TexCoord1, shattering_uv);
            renderer.sprite.SetVertexAttribute(VertexAttribute.TexCoord2, default_uv);
            
            renderer.material.SetVector("_UVWidth", new Vector4(uv[0].x + 0.0025f, uv[1].x - 0.0025f));
            renderer.material.SetVector("_UVHeight", new Vector4(uv[2].y + 0.0025f, uv[0].y - 0.0025f));
            renderer.transform.localScale *= 2.0f;
            renderer.transform.localPosition *= 2.0f;
        }

        foreach (var text in GetComponentsInChildren<TextMeshPro>())
        {
            if (text.name != "Text")
                continue;

            text.AddComponent<Destroyer>();
            text.AddComponent<Animation>();
            Animation animation = text.GetComponent<Animation>();
            animation.AddClip(destroy_animation, "DestroyAnimation");
            animation.Play("DestroyAnimation");
        }
    }
}