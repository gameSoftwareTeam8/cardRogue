using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCard : MonoBehaviour
{
    public GameObject parentObject;
    public void ExitDeck()
    {

        if (parentObject != null)
        {
            foreach (Transform child in parentObject.transform)
            {   
                Debug.Log(child.name);
                if (child.name != "GameTitle" && child.name != "ContentBackground")
                {
                    Debug.Log("destroy deck card");
                    Destroy(child.gameObject);
                }
                    

            }
        }
    }

    public void ExitCard()
    {
        GameObject parentObject = GameObject.Find("PopCardImage");
       
        if(parentObject != null)
        {
            Debug.Log("Exit Card Information");
            foreach (Transform child in parentObject.transform)
                Destroy(child.gameObject);
        }
        
   
    }
}
