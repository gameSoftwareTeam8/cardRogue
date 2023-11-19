using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.Mathematics;
using System;
using UnityEngine.SceneManagement; 

public class LoadDeck : MonoBehaviour
{
    public GameObject content;
    public GameObject contentBackground;
    public GameObject scrollView;
    public Transform gridTransform;

    float contentHeight = 980f;

    public void DisplayCards()
    {

        IPlayer player = Locator.player;

        RectTransform contentRect = content.GetComponent<RectTransform>();

        RectTransform contentBackgroundRect = contentBackground.GetComponent<RectTransform>();

        RectTransform scrollRect = scrollView.GetComponent<RectTransform>();

        double cardCount = Math.Ceiling(player.cards_count / 4.0); 
        
        if (cardCount > 2) 
        {
            contentHeight += (float)((cardCount - 2) * 425);
            Debug.Log(contentHeight);
        }
        
        contentRect.sizeDelta = new Vector2(1450, contentHeight);

        contentRect.anchorMin = new Vector2(0.5f, 1f);
        contentRect.anchorMax = new Vector2(0.5f, 1f);
        contentRect.pivot = new Vector2(0.5f, 1f);

        contentBackgroundRect.sizeDelta = new Vector2(1450, contentHeight);

        scrollRect.sizeDelta = new Vector2(1500, contentHeight+20);

        for (int i = 0; i < player.cards_count; i++)
        {
            Card card = player.get_card(i);
            Debug.Log(card);

            card.transform.localScale = new Vector3(75, 75, 1);

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
                Debug.Log("67 rect transform not found");
                rectTransform = cardButton.AddComponent<RectTransform>();
            }

            int row = (i / 4) + 1;
            int column = i % 4;
            float cardWidth = 265;
            float cardHeight = 370;

            float x = -514.5f;
            float y = -325f;
            // 

            rectTransform.sizeDelta = new Vector2(cardWidth, cardHeight);

            x += i%4 * 343;
            
            if(i/4==1) y -= 425;

  
            rectTransform.anchoredPosition = new Vector2(x, y);
            rectTransform.anchorMin = new Vector2(0.5f, 1);
            rectTransform.anchorMax = new Vector2(0.5f, 1);

            card.GetComponent<CardView>().show();


            button.onClick.AddListener(() => OnCardClicked(card.transform.parent.gameObject));

            
        }
        

    }

    public void OnCardClicked(GameObject cardButton)
    {

        GameObject scrollView = GameObject.Find("Deck");

        GameObject cardObject = Instantiate(cardButton.gameObject, gridTransform);

        RectTransform rectTransform = cardObject.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(0, 0);

        cardObject.transform.localScale = new Vector3(1.5f, 1.5f, 1);

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