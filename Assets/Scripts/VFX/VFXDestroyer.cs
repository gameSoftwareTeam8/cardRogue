using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class VFXDestroyer : MonoBehaviour
{
    VisualEffect visual_effect;
    float current_time = 0.0f;
    void Start()
    {
        visual_effect = GetComponent<VisualEffect>();
    }

    void FixedUpdate()
    {
        current_time += Time.deltaTime;
        // if (!visual_effect.HasAnySystemAwake()) {
        if (current_time >= 15.0f) {
            Destroy(gameObject);
        }
    }
}
