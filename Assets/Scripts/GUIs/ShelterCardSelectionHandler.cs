using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShelterCardSelectionHandler : MonoBehaviour
{
    public GameObject deckScrollViewObject;
    public GameObject selectedCardObject;
    public Button button;
    public GameObject leftParticle;
    public GameObject rightParticle;
    public GameObject SmokeParticle;
    public Material material;

    public Player player;

    public void OnCardClicked()
    {
        ActivateParticle(button);
    }

    private void ActivateParticle(Button buttonClicked)
    {
        if(buttonClicked.name == "HealCard")
        {
            leftParticle.transform.localScale = new Vector3(18,18,18);
            rightParticle.SetActive(false);
        }
        else if(buttonClicked.name == "DeckRemoveCard")
        {
            StartCoroutine(UsingTime());

            rightParticle.transform.localScale = new Vector3(18,18,18);
            leftParticle.SetActive(false);
       }
        SmokeParticle.SetActive(true);       
        
    }

    private void CardScaler(Button button)
    {   
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        

        if (button.name == "HealCard")
        {
            rectTransform.anchoredPosition = new Vector2(-500, 50); 
            GameObject leftFire = GameObject.Find("LeftCampfire");
            leftFire.transform.localScale = new Vector3(300,200,0);
        }
        else if(button.name == "DeckRemoveCard")
        {
            rectTransform.anchoredPosition = new Vector2(500, 50); 
            GameObject leftFire = GameObject.Find("RightCampfire");
            leftFire.transform.localScale = new Vector3(300,200,0);
        }
        
        rectTransform.localScale = new Vector2(2f, 2f);
    }

    private void ActiveCard(Button button)
    {
        IPlayer player = Locator.player;
        if (button.name == "HealCard")
        {
            //체력
            player.heal(5);

            GameObject destoryButton = GameObject.Find("HealCard");
            Destroy(destoryButton.GetComponent<Button>());
        }
        else if (button.name == "DeckRemoveCard")
        {
            // 카드 제거
            // 덱에 있는 카드 다시 보여주고 클릭하면 확인 창 뜨게 하고
            // 확인하면 삭제 취소하면 다시
            // 확인하면 그 골랐던 카드를 삭제
            //Card removedCard = Card.
            //player.remove_card();
            
            GameObject destoryButton = GameObject.Find("DeckRemoveCard");
            GameObject destoryButton2 = GameObject.Find("HealCard");
            GameObject destoryWood = GameObject.Find("LeftCampfire");
            Destroy(destoryButton);
            Destroy(destoryButton2);
            Destroy(destoryWood);

            deckScrollViewObject.SetActive(true);
            selectedCardObject.SetActive(true);

            material.SetFloat("_Age", 7);
        }

    }

    IEnumerator UsingTime()
    {
        yield return new WaitForSeconds(2.7f);

        CardScaler(button);
        ActiveCard(button);
    }
    
}
