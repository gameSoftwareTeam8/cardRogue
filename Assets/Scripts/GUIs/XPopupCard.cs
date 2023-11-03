using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPopupCard : MonoBehaviour
{

    public void OnXButtonClicked()
    {
        GameObject parentObj = GameObject.Find("PopCardImage");

        Transform childObj = parentObj.transform.GetChild(0);

        Destroy(childObj.gameObject);

        GameObject scrollView = GameObject.Find("Scroll View");
        if (scrollView != null)
        {
            scrollView.SetActive(true);
        }
    }
}
