using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class savingRewardCArd : MonoBehaviour
{/*
    IPlayer player = Locator.player;
    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject thirdCard;

    private GameObject rewardCard;
    private const int CARDS_NUM = 3;

    int idx = 1;

    void Start()
    {
        RewardCardShow();
    }
    void RewardCardShow()
    {
        var card_factory = Locator.card_factory;
        var sampled = Locator.card_pool.sample_without_replacement(CARDS_NUM);
        foreach (CardInfo info in sampled)
        {
            rewardCard = card_factory.create(info);

            rewardCard.transform.localScale = new Vector3(120, 120, 1);

            
            RectTransform rectTransform = rewardCard.AddComponent<RectTransform>();
            
            GameObject cardButton = new GameObject(rewardCard.name + "Button");
            Button button = cardButton.AddComponent<Button>();

            button.onClick.AddListener(() => SelectCard(rewardCard.GetComponent<Card>()));
            

            if(idx==1)
                rewardCard.transform.SetParent(firstCard.transform, false);
            else if(idx==2)
                rewardCard.transform.SetParent(secondCard.transform, false);
            else if(idx==3)
                rewardCard.transform.SetParent(thirdCard.transform, false);

            rewardCard.transform.localScale = new Vector3(100,100,1);
            
            
            idx++;

            rewardCard.GetComponent<CardView>().show();
        }

    }
    
    public void SelectCard(Card card)
    {
        player.add_card(card);

        SceneManager.LoadScene("Map");
    }

    */
}

