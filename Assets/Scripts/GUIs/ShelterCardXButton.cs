using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterCardXButton : MonoBehaviour
{   
    public Transform parentObject;

    public void Xbutton()
    {
        ShelterBool.is_exists = false;
        Transform firstChildTransform = parentObject.GetChild(4);
        Destroy(firstChildTransform.gameObject);
    }
}
