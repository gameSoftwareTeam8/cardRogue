using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsControl : MonoBehaviour
{
    public GameObject sender;

    void OnCreate() { sender.SetActive(true); }

    void OnDamage() { Destroy(sender); }

    void OnBurn() { Destroy(sender); }

    void OnDead()
    {
        if(sender== null)
            FindObjectOfType<MouseControl>().AddCard();
        Destroy(gameObject);
    }
}
