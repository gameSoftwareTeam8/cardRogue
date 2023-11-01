using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PopupSystem : MonoBehaviour
{
    public GameObject popup;
    Animator anima;

    Action onClickCancle;

    public static PopupSystem instance{get;  private set;}

    private void Awake()
    {
        instance = this;
        anima = popup.GetComponent<Animator>();
    }

    private void Update()
    {
        if (popup.name == "OptionMenu" && Input.GetButtonDown("Cancel"))
        {
            if (popup.activeSelf)
                popup.SetActive(false);
            else
                popup.SetActive(true);

        }

        if (anima.GetCurrentAnimatorStateInfo(0).IsName("Close"))
        { 
            if(anima.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                popup.SetActive(false);
            }
        }
    }

    public void OpenPopup(
        Action onClickCancle
        )
    {

        this.onClickCancle = onClickCancle;
        popup.SetActive(true);
    }


    public void OnClickCancle()
    { 
        if(onClickCancle != null)
        {
            onClickCancle();
        }

        ClosePopup();
    }

    void ClosePopup()
    {
        anima.SetTrigger("close");
    }
}

