using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class VFXDestroyer : MonoBehaviour
{
    VisualEffect visual_effect;
    void Start()
    {
        visual_effect = GetComponent<VisualEffect>();
    }

    void FixedUpdate()
    {
        // if (!visual_effect.HasAnySystemAwake()) {
        //     Debug.Log(visual_effect);
        //     Destroy(gameObject);
        // }
    }
}
