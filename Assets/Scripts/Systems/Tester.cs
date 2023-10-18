using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tester: MonoBehaviour
{
    public float time = 0.0f;
    void Start()
    {
        GetComponent<CardView>().show();
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time >= 3.0f)
            GetComponent<Card>().destroy();
    }
}