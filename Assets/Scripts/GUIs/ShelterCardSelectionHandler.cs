using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class ShelterCardSelectionHandler : MonoBehaviour
{
    public Button button;
    public GameObject leftParticle;
    public GameObject rightParticle;
    public GameObject SmokeParticle;


    public void OnCardClicked()
    {
        ActivateParticle(button);
        ButtonScaler(button);
        ActiveCard(button);
    }

    private void ActivateParticle(Button buttonClicked)
    {
        if(buttonClicked.name == "LeftCardImage")
        {
            leftParticle.transform.localScale = new Vector3(18,18,18);
            rightParticle.SetActive(false);
        }
        else if(buttonClicked.name == "RightCardImage")
        {
            rightParticle.transform.localScale = new Vector3(18,18,18);
            leftParticle.SetActive(false);
       }
        SmokeParticle.SetActive(true);       
        
    }

    private void ButtonScaler(Button button)
    {   
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        

        if (button.name == "LeftCardImage")
        {
            rectTransform.anchoredPosition = new Vector2(220, 50); 
            GameObject leftFire = GameObject.Find("LeftCampfire");
            leftFire.transform.localScale = new Vector3(300,200,0);
        }
        else if(button.name == "RightCardImage")
        {
            rectTransform.anchoredPosition = new Vector2(-220, 50); 
            GameObject leftFire = GameObject.Find("RightCampfire");
            leftFire.transform.localScale = new Vector3(300,200,0);
        }
        
        rectTransform.localScale = new Vector2(1.3f, 1.3f);
    }

    private void ActiveCard(Button button)
    {
        if (button.name == "LeftCardImage")
        {
            //체력
        }
        else if (button.name == "RightCardImage")
        {
            //강화
        }

    }
    
        
}
