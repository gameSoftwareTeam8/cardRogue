using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DontDestroyer : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
