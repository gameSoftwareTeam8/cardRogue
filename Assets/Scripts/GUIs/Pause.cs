using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public GameObject PausePanel;

    bool isPaused;

   
    public void PauseGame()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;

        //audio pause
        //AudioListe
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;

        AudioListener.pause = false;
    }



}

