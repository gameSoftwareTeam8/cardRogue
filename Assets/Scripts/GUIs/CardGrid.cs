using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.Mathematics;
using System;

public class CardGrid : MonoBehaviour
{
    public Transform gridTransform;


    void Start()
    {
        DisplayCards();
    }

    void DisplayCards()
    {
        IPlayer player = Locator.player;
        
        GameObject content = GameObject.Find("Content");

        GameObject contentBackground = GameObject.Find("ContentBackground");

        RectTransform contentRect = content.GetComponent<RectTransform>();

        RectTransform contentBackgroundRect = contentBackground.GetComponent<RectTransform>();

        float contentHeight = 1080f;

        double cardCount = Math.Ceiling(player.cards_count / 4.0); 
        
        if (cardCount > 2) 
        {
            contentHeight = contentHeight + (float)((cardCount - 2) * 400);
        }
        
        contentRect.sizeDelta = new Vector2(1450, contentHeight);
        contentBackgroundRect.sizeDelta = new Vector2(1450, contentHeight);

        for (int i = 0; i < player.cards_count; i++)
        {
            // card

            Card card = player.get_card(i);

            card.transform.localScale = new Vector3(75, 75, 1);

            // card button

            GameObject cardButton = new GameObject(card.name + "Button");

            Image image = cardButton.AddComponent<Image>();

            Button button = cardButton.AddComponent<Button>();

            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);

            button.onClick.AddListener(() => OnClickMyButton());


            //

            cardButton.transform.SetParent(content.transform, false);

            card.transform.SetParent(cardButton.transform, false);

            RectTransform rectTransform = cardButton.GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                rectTransform = cardButton.AddComponent<RectTransform>();
            }

            int row = (i / 4) + 1;
            int column = i % 4;
            float cardWidth = 265*0.75f;
            float cardHeight = 370*0.75f;
            float spacing = 120f;

            float x;
            float y = -440;
            

            rectTransform.sizeDelta = new Vector2(cardWidth, cardHeight);

            x = (column * (cardWidth + spacing)) - (1920f / 2) + (cardWidth / 2) + spacing + 260 ;
            
            if(i/4==1) y -= 400;


  
            rectTransform.anchoredPosition = new Vector2(x, y);
            rectTransform.anchorMin = new Vector2(0.5f, 1);
            rectTransform.anchorMax = new Vector2(0.5f, 1);

   
            card.GetComponent<CardView>().show();


            button.onClick.AddListener(() => OnCardClicked(card.transform.parent.gameObject));

            
        }

    }


    public void OnCardClicked(GameObject cardButton)
    {

        GameObject scrollView = GameObject.Find("Scroll View");
        if (scrollView != null)
        { 
            scrollView.SetActive(false);
        }

        GameObject cardObject = Instantiate(cardButton.gameObject, gridTransform);

        RectTransform rectTransform = cardObject.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(0, 0);

        cardObject.transform.localScale = new Vector3(1.5f, 1.5f, 1);

        Debug.Log(cardObject.name);
        

        GameObject popCardImageGameObject = GameObject.Find("PopCardImage");

        cardObject.transform.SetParent(popCardImageGameObject.transform, false);

        if (popCardImageGameObject != null)
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
        }

        
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

