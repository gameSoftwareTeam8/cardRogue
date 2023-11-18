using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCard : MonoBehaviour
{
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
}

//sprite material?
//Rect transform (position, scale, )
//Script(destroyTarget 지정, material 지정)
//material
//button (onclick)
