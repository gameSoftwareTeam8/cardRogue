using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public GameObject PuasePanel;

   
    public void PauseGame()
    {
        PuasePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        PuasePanel.SetActive(false);
        Time.timeScale = 1;
    }
}

