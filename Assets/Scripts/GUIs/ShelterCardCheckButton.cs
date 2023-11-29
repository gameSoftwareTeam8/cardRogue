using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterCardCheckButton : MonoBehaviour
{
    public GameObject particle;
    //public

    public void CheckButton()
    {
        IPlayer player = Locator.player;

        /*
        for (int i = 0; i < player.cards_count; i++)
        {
            string check = ShelterBool.remove_card.name + "(clone)";
            if(check==player.get_card(i).name)
            {   
                player.remove_card(player.get_original_card(i));
                break;
            }
        
        }
        for (int i = 0; i < player.cards_count; i++)
        {
            Card card = player.get_original_card(i);
            Debug.Log(card);
        }
        */

        StartCoroutine(WaitParticle());
    }

    IEnumerator WaitParticle()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeManager.Instance.LoadDiffScene("HeaderBar"));
    }
}
