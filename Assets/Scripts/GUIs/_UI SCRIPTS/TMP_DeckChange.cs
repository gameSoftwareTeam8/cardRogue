using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TMP_DeckChange : MonoBehaviour
{
    public void ToDeckScene()
    {
        SceneManager.LoadScene("DeckScene");
    }
}
