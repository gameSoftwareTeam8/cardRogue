using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager: MonoBehaviour
{
    public List<EnemyInfo> enemies;
    void Awake()
    {
        Locator.enemy_manager = this;
    }

    public void create_enemy()
    {
        IBoard board = Locator.board;
        CardFactory card_factory = Locator.card_factory;

        EnemyInfo enemy = enemies[Random.Range(0, enemies.Count - 1)];
        CreatureInfo[] cards = new CreatureInfo[]{ enemy.left, enemy.center, enemy.right };
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] == null)
                continue;

            Creature creature = card_factory.create(cards[i]).GetComponent<Creature>();
            creature.transform.localScale = new Vector3(1.875f, 1.875f, 1.0f);
            board.add_card(BoardSide.AWAY, i, creature);
        }
    }
}