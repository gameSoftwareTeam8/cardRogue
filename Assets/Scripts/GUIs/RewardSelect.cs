using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RewardSelect : MonoBehaviour
{
    public GameObject rewardCardObj;

    public void addCard(Card card)
    {
        IPlayer player = Locator.player;
        CardFactory card_factory = Locator.card_factory;
        player.add_card(card_factory.create(card.info).GetComponent<Card>());
        StartCoroutine(WaitTime());
    }
    
    public void SelectFirstCard()
    {
        RewardCard rewardCard = rewardCardObj.GetComponent<RewardCard>();
        addCard(rewardCard.card1);
    }

    public void SelectSecondCard()
    {
        RewardCard rewardCard = rewardCardObj.GetComponent<RewardCard>();
        addCard(rewardCard.card2);
    }
    
    public void SelectThirdCard()
    {
        RewardCard rewardCard = rewardCardObj.GetComponent<RewardCard>();
        addCard(rewardCard.card3);
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeManager.Instance.LoadDiffScene("Map"));
    }
}
