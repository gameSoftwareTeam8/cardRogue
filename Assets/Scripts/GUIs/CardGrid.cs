using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        for (int i = 0; i < player.cards_count; i++)
        {
            // card

            Card card = player.get_card(i);

            card.transform.localScale = new Vector3(100, 100, 1);

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

            int row = i / 4;
            int column = i % 4;
            float cardWidth = 265f;
            float cardHeight = 370f;
            float spacing = 172f;

            rectTransform.sizeDelta = new Vector2(cardWidth, cardHeight);

            float x = (column * (cardWidth + spacing)) - (1920f / 2) + (cardWidth / 2) + spacing;
            float y = -(row * (cardHeight + spacing)) + ((cardHeight + spacing) / 2) - 300f;
  
            rectTransform.anchoredPosition = new Vector2(x, y);

   
            card.GetComponent<CardView>().show();


            button.onClick.AddListener(() => OnCardClicked(card.transform.parent.gameObject));

            
        }

    }


    public void OnCardClicked(GameObject card)
    {

        GameObject scrollView = GameObject.Find("Scroll View");
        if (scrollView != null)
        { 
            scrollView.SetActive(false);
        }

        GameObject cardObject = Instantiate(card.gameObject, gridTransform);

        RectTransform rectTransform = cardObject.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(0, 0);

        cardObject.transform.localScale = new Vector3(1.5f, 1.5f, 1);

        GameObject popCardImageGameObject = GameObject.Find("PopCardImage");

        cardObject.transform.SetParent(popCardImageGameObject.transform, false);

        if (popCardImageGameObject != null)
        {

            Image popCardImage = cardObject.GetComponent<Image>();

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

