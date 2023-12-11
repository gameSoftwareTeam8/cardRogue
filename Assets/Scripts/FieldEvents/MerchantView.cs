using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MerchantView: MonoBehaviour
{   
    public List<Transform> card_zones = new();
    private IMerchant merchant;
    private GameObject price_tag_prefab;
    void Awake()
    {
        merchant = new Merchant();
        price_tag_prefab = Resources.Load<GameObject>("Prefabs/Cards/PriceTag");
        for (int i = 0; i < merchant.cards_size; i++)
        {
            Card card = merchant.get_card(i);
            if (i < card_zones.Count) {
                card.transform.position = card_zones[i].position;
                CardView card_view = card.GetComponent<CardView>();
                card_view.show();
                card_view.on_mouse_down += on_card_cliked;

                GameObject price_object = Instantiate(price_tag_prefab);
                price_object.transform.SetParent(card.transform, false);
                price_object.GetComponent<PriceTag>().price = card.info.price;
            }
        }
    }

    public void on_card_cliked(object sender, EventArgs args)
    {
        CardView card_view = (CardView)sender;
        Card card = card_view.GetComponent<Card>();
        if (merchant.buy(card)) {
            card_view.on_mouse_down -= on_card_cliked;
            card_view.hide();
            Destroy(card.transform.Find("PriceTag(Clone)").gameObject);
            Debug.Log("Bought");
        }
        else
            Debug.Log("Not enough money");
    }
}