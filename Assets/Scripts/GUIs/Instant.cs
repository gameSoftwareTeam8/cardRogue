using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instant : MonoBehaviour
{
    public GameObject pref;

    void Start()
    {
        GameObject newPref = Instantiate(pref, transform.position, transform.rotation) as GameObject;
        newPref.transform.SetParent(GameObject.FindGameObjectWithTag("aaa").transform, false);
    }
 
   
}
