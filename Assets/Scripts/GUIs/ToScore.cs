using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToScore : MonoBehaviour
{
    public void ToShowScore()
    {
        StartCoroutine(FadeManager.Instance.LoadDiffScene("ShowScore"));
    }

}
