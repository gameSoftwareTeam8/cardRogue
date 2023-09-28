using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager: MonoBehaviour
{
    public Player player;
    public Board board;
    public TurnManager turn_manager = new();
    public List<CardInfo> test_card_info;
    void Awake()
    {
        Locator.player = player;
        Locator.board = board;
        board.init();
        board.GetComponent<BoardView>().init();

        foreach (var info in test_card_info)
        {
            GameObject card_object = Locator.card_factory.create(info);
            Locator.player.add_card(card_object);
        }

        board.add_card(BoardSide.HOME, 0, Locator.card_factory.create(test_card_info[0]).GetComponent<Creature>());
        board.add_card(BoardSide.HOME, 1, Locator.card_factory.create(test_card_info[2]).GetComponent<Creature>());
        board.add_card(BoardSide.AWAY, 0, Locator.card_factory.create(test_card_info[1]).GetComponent<Creature>());
        board.add_card(BoardSide.AWAY, 1, Locator.card_factory.create(test_card_info[0]).GetComponent<Creature>());
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
            turn_manager.battle();
    }
}