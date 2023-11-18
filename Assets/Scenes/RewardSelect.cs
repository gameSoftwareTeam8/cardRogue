using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RewardSelect : MonoBehaviour
{
    public GameObject rewardCardObj;

    public void SelectFirstCard()
    {
        IPlayer player = Locator.player;

        RewardCard rewardCard = rewardCardObj.GetComponent<RewardCard>();


        // ScriptA의 변수에 접근합니다.
        Card addedCard = rewardCard.card1;

        player.add_card(addedCard);
        
        StartCoroutine(WaitTime());
    }

    public void SelectSecondCard()
    {
        IPlayer player = Locator.player;

        RewardCard rewardCard = rewardCardObj.GetComponent<RewardCard>();


        // ScriptA의 변수에 접근합니다.
        Card addedCard = rewardCard.card2;

        player.add_card(addedCard);

        StartCoroutine(WaitTime());
    }
    
    public void SelectThirdCard()
    {
        IPlayer player = Locator.player;

        RewardCard rewardCard = rewardCardObj.GetComponent<RewardCard>();


        // ScriptA의 변수에 접근합니다.
        Card addedCard = rewardCard.card3;

        player.add_card(addedCard);
        
        StartCoroutine(WaitTime());
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene("HeaderBar");
    }
}
