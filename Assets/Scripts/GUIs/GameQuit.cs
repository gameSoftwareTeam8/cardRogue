using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuit : MonoBehaviour
{ 
    public void QuitGame()
    {
        Debug.Log("QUIT"); 
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
