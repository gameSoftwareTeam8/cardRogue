using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToHeaderBar : MonoBehaviour
{
     public void ToHeader()
     {
        StartCoroutine(FadeManager.Instance.LoadDiffScene("HeaderBar"));
     }
}
