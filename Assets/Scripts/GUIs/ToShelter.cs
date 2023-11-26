using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToShelter : MonoBehaviour
{
    public void GoToShelter()
    {
        SceneManager.LoadScene("Shelter");
        Debug.Log("Shelter");
    }
}
