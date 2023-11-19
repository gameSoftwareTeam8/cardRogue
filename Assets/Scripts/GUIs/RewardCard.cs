using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RewardCard : MonoBehaviour
{
    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject thirdCard;

    public Card card1, card2, card3;


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


            if(idx==1)
                rewardCard.transform.SetParent(firstCard.transform, false);
            else if(idx==2)
                rewardCard.transform.SetParent(secondCard.transform, false);
            else if(idx==3)
                rewardCard.transform.SetParent(thirdCard.transform, false);

            // get parent
            Transform myTransform = rewardCard.transform;

            Transform parentTransform = myTransform.parent;

            Button parentButton = parentTransform.GetComponent<Button>();

            RectTransform rectTransform = rewardCard.AddComponent<RectTransform>();
        

            if (idx == 1)
                card1 = rewardCard.GetComponent<Card>();
            else if (idx == 2)
                card2 = rewardCard.GetComponent<Card>();
            else if (idx == 3)
                card3 = rewardCard.GetComponent<Card>();
            


            rewardCard.transform.localScale = new Vector3(130,130,1);
            
            
            idx++;

            rewardCard.GetComponent<CardView>().show();

        }

    }
    
}

