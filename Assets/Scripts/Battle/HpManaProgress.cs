using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManaProgress : MonoBehaviour
{
    public Text Hp;
    public Image ManaBar;
    public Image HpBar;
    public float speed;
    float ManaPercent;
    IPlayer player = Locator.player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Hp.text = player.hp.ToString();
        HpBar.fillAmount = player.hp / player.max_hp;
        ManaPercent = player.mana / player.max_mana;
        ManaBar.fillAmount = ManaPercent * 100;
    }
}
