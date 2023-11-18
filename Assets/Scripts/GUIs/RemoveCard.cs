using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCard : MonoBehaviour
{
    public void ExitDeck()
    {
 
        GameObject parentObj = GameObject.Find("Content");

        if (parentObj != null)
        {
            foreach (Transform child in parentObj.transform)
            {
                if (child.name != "GameTitle")
                {
                    Debug.Log("destroy deck card");
                    Destroy(child.gameObject);
                }
                    

            }
        }
    }

    public void ExitCard()
    {
        GameObject parentObj = GameObject.Find("PopCardImage");
       
        if(parentObj != null)
        {
            Debug.Log("Exit Card Information");
            foreach (Transform child in parentObj.transform)
                Destroy(child.gameObject);
        }
        
   
    }
}
