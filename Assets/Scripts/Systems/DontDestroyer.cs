using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DontDestroyer : MonoBehaviour
{
    public Canvas myCanvas;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetCanvasActive(bool isActive)
    {
        myCanvas.gameObject.SetActive(isActive);
    }
}
