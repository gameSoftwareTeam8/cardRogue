using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ToDeck : MonoBehaviour
{

    public void SceneChange()
    {
        SceneManager.LoadScene("DeckScene");
    }
}
