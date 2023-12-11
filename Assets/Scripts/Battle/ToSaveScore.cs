using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSaveScore : MonoBehaviour
{
    void Awake()
    {
        foreach (var item in FindObjectsOfType<DontDestroyer>())
            Destroy(item.gameObject);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("SaveScore");
        }
    }
}
