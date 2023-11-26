using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShelterPopup : MonoBehaviour
{
    public GameObject checkButton;
    public GameObject xButton;
    public GameObject text;
    public GameObject effectText;
    public GameObject hpImage;

    public void ShelterCardClicked()
    {
        RemoveScaler();
        checkButton.SetActive(true);
        xButton.SetActive(true);
        effectText.SetActive(true);
        if(hpImage != null)
            hpImage.SetActive(true);

        text.SetActive(false);
    }

    void RemoveScaler()
    {
        ButtonScaler buttonScaler = GetComponent<ButtonScaler>();
        Destroy(buttonScaler);
    }
}
