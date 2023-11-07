using System;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneChecker : MonoBehaviour
{
    public GameObject QButton; 
    public GameObject QPanel;

    string currentSceneName;

    void Update()
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Map")
        {
            QButton.SetActive(true);
        }
        else
        {
            QButton.SetActive(false);
            QPanel.SetActive(false);
        }
    }
}
