using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TMP_UIManager : MonoBehaviour
{
    public GameObject popupSet;
    

    void Update()
    {
        
        //Pause Menu
        if (Input.GetButtonDown("Cancel"))
        {
            if (popupSet.activeSelf)
                popupSet.SetActive(false);
            else
                popupSet.SetActive(true);

        }
    }

    public void GameSave()
    {

    }

    public void GameExit()
    {
        Debug.Log("QUIT"); 
        Application.Quit();
    }
}
