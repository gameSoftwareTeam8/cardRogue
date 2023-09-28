using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class CardVfx: MonoBehaviour
{
    [SerializeField]
    private Material burning_material;

    public void Start()
    {
        
    }
    public void on_destroyed()
    {
        GameObject smoke_object = Locator.vfx_factory.create("Smoke");
        smoke_object.transform.parent = transform;
        smoke_object.transform.localPosition = Vector3.zero;
        // List<Vector2> uv = transform.Find("Background").GetComponent<SpriteRenderer>().sprite.uv.ToList();

        Vector3 base_size = transform.Find("Background").GetComponent<SpriteRenderer>().sprite.bounds.size;
        float width = base_size.x;
        float height = base_size.y;
        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            if (renderer.sprite == null)
                continue;
            
            renderer.material = burning_material;
            Vector3 size = renderer.sprite.bounds.size;
            var material_block = new MaterialPropertyBlock();
            material_block.SetTexture("_MainTex", renderer.sprite.texture);
            // material_block.SetVector("_Scale", new Vector2(size.x / width, size.y / height));
            // material_block.SetVector("_Offset", new Vector2(pos.x / width, pos.y / height));
            Vector3 pos = renderer.transform.localPosition;
            renderer.SetPropertyBlock(material_block);
        }
    }
}