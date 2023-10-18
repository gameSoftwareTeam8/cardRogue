using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShatteringVFX : MonoBehaviour
{
    public bool run = false;
    bool is_running = false;
    SpriteRenderer sprite_renderer;
    public Material material;
    void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (run) {
            sprite_renderer.material = material;
            run = false;
            is_running = true;
        }
        if (!is_running)
            return;
        
        var age = sprite_renderer.material.GetFloat("_Age");
        sprite_renderer.material.SetFloat("_Age", age + Time.deltaTime);
        var uv = sprite_renderer.sprite.uv;
        sprite_renderer.material.SetVector("_UVWidth", new Vector4(uv[0].x + 0.0025f, uv[1].x - 0.0025f));
        sprite_renderer.material.SetVector("_UVHeight", new Vector4(uv[2].y + 0.0025f, uv[0].y - 0.0025f));
    }
}
