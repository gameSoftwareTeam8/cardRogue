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
        player.remove_card(ShelterBool.remove_card);
        Destroy(ShelterBool.remove_card.gameObject);
        StartCoroutine(WaitParticle());
    }

    IEnumerator WaitParticle()
    {
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(FadeManager.Instance.LoadDiffScene("Map"));
    }
}
