using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShatteringVFX : MonoBehaviour
{
    SpriteRenderer sprite_renderer;
    void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        var uv = sprite_renderer.sprite.uv;
        Debug.Log(uv[0]);
        Debug.Log(uv[1]);
        Debug.Log(uv[2]);
        Debug.Log(uv[3]);
        sprite_renderer.material.SetVector("_UVWidth", new Vector4(uv[0].x + 0.0025f, uv[1].x - 0.0025f));
        sprite_renderer.material.SetVector("_UVHeight", new Vector4(uv[2].y + 0.0025f, uv[0].y - 0.0025f));
    }
}
