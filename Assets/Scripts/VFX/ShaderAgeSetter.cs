using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShaderAgeSetter : MonoBehaviour
{
    SpriteRenderer sprite_renderer;
    void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
    }
    
    void FixedUpdate()
    {
        var age = sprite_renderer.material.GetFloat("_Age");
        if (age >= 10.0f)
            Destroy(gameObject);
        sprite_renderer.material.SetFloat("_Age", age + Time.deltaTime);
    }   
}
