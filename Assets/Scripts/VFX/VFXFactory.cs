using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;


public class VFXFactory
{
    private Dictionary<string, VisualEffectAsset> vfxes = new();
    public VFXFactory()
    {
        foreach (VisualEffectAsset vfx_asset in Resources.LoadAll<VisualEffectAsset>("VFX"))
            vfxes[vfx_asset.name] = vfx_asset;
    }

    public GameObject create(string vfx_name)
    {
        if (!vfxes.ContainsKey(vfx_name))
            return null;

        GameObject vfx_object = new GameObject();
        vfx_object.AddComponent<VisualEffect>();
        VisualEffect vfx = vfx_object.GetComponent<VisualEffect>();
        vfx.visualEffectAsset = vfxes[vfx_name];
        vfx.GetComponent<Renderer>().sortingLayerName = "VFX";
        vfx_object.AddComponent<VFXDestroyer>();

        return vfx_object;
    }
}
