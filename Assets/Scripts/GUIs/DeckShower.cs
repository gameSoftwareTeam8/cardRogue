using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeckShower : MonoBehaviour
{
    public int card_width = 5, card_height = 7;
    public int column = 5;
    private void Start()
    {
        show_cards();
    }

    public void show_cards()
    {
        IPlayer player = Locator.player;
        
        for (int i = 0; i < player.cards_count; i++)
        {
            Vector3 position = transform.localPosition
                             + Vector3.right * (i % column) * card_width
                             + Vector3.down * (i / column) * card_height;
            player.get_card(i).transform.position = position;
        }
    }
}