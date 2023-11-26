using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;


public class DeckEliminator : MonoBehaviour
{
    public Transform gridTransform;
    public GameObject deckScrollViewObject; 
    public GameObject contentObject;
    public GameObject selectedCardObject;
    public GameObject alreadyChosenObject;
    public GameObject cardFireParticleObject;
    

    public void DisplayCards()
    {
        IPlayer player = Locator.player;
        
        GameObject content = contentObject;

        RectTransform contentRect = content.GetComponent<RectTransform>();

        float y = -300;

        for (int i = 0; i < player.cards_count; i++)
        {
            Card card = player.get_card(i);

            card.transform.localScale = new Vector3(75, 75, 1);

            // card button

            GameObject cardButton = new GameObject(card.name + "Button");

            Image image = cardButton.AddComponent<Image>();

            Button button = cardButton.AddComponent<Button>();

            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);


            //

            cardButton.transform.SetParent(content.transform, false);

            card.transform.SetParent(cardButton.transform, false);

            RectTransform rectTransform = cardButton.GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                rectTransform = cardButton.AddComponent<RectTransform>();
            }



            int row = (i / 2) + 1;
            int column = i % 4;
            float cardWidth = 265;
            float cardHeight = 370;
            
            float x = i % 2 == 0 ? 280 : 580;
            

            if(i!=0 && i%2==0)
            {
                y -= 370;
            }

            rectTransform.sizeDelta = new Vector2(cardWidth, cardHeight);
            rectTransform.anchoredPosition = new Vector2(x, y);
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);

            deckScrollViewObject.SetActive(true);
            selectedCardObject.SetActive(true);
            cardFireParticleObject.SetActive(true); 

            RectTransform contentRectTransform = contentObject.GetComponent<RectTransform>();
            contentRectTransform.sizeDelta = new Vector2(863, row*400);

   
            card.GetComponent<CardView>().show();

            
            button.onClick.AddListener(() => OnCardClicked(card.transform.parent.gameObject, card));


            
        }

    }


    public void OnCardClicked(GameObject cardButton, Card card)
    {
        if (ShelterBool.is_exists == false)
        {
            GameObject cardObject = Instantiate(cardButton.gameObject, gridTransform);

            RectTransform rectTransform = cardObject.GetComponent<RectTransform>();

            rectTransform.anchoredPosition = new Vector2(0, 0);

            cardObject.transform.localScale = new Vector3(2f, 2f, 1);

            cardObject.transform.SetParent(selectedCardObject.transform, false);

            if (selectedCardObject != null)
            {
               Image popCardImage = cardObject.GetComponent<Image>();
            }

            Transform parentTransform = cardObject.transform; // 부모 오브젝트의 Transform 컴포넌트
            if (parentTransform.childCount > 0)
            {
                Transform firstChildTransform = parentTransform.GetChild(0);

                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);

                firstChildTransform.GetComponent<CardView>().show();

                Debug.Log("FirstChild: " + firstChildTransform);
            }
            ShelterBool.is_exists = true;

            ShelterBool.remove_card = card;
        }
        else
        {
            alreadyChosenObject.SetActive(true);
            StartCoroutine(NotifyExists());
        }

    }

    IEnumerator NotifyExists()
    {
        yield return new WaitForSeconds(0.5f);
        alreadyChosenObject.SetActive(false);
    }
    
    
    public void OnClickMyButton()
    {
        PopupSystem.instance.OpenPopup(
            () =>
            {
            }
            );
    }

}

