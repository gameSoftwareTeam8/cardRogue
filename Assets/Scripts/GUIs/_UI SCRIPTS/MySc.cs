using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySc : MonoBehaviour
{
    public void OnClickMyButton()
    {
        PopupSystem.instance.OpenPopup(
            () =>
            {
                Debug.Log("OnClickOK");
            },
            () =>
            {
                Debug.Log("OnClickCancle");
            }
            );
    }

}
