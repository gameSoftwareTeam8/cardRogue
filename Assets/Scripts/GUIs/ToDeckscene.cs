using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToDeckScene : MonoBehaviour
{
    public void SceneChange()
    {
        //GameObject eventObject = GameObject.Find("HeaderBarEventSystem");
        //eventObject.SetActive(false);

        SceneManager.LoadScene("DeckScene");
    }
}
