using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager: MonoBehaviour
{
    public static GameManager Inst { get; private set; }
    public Board board;
    public TurnManager turn_manager = new();
    public List<CardInfo> test_card_info;
    [SerializeField] NotificationPanel notificationPanel;
    void Awake()
    {
        Inst = this;
        Locator.board = board;
        board.init();
        board.GetComponent<BoardView>().init();

        foreach (var info in test_card_info)
        {
            GameObject card_object = Locator.card_factory.create(info);
            Locator.player.add_card(card_object);
        }

        board.add_card(BoardSide.HOME, 1, Locator.card_factory.create(test_card_info[2]).GetComponent<Creature>());
        board.add_card(BoardSide.AWAY, 0, Locator.card_factory.create(test_card_info[1]).GetComponent<Creature>());
        board.add_card(BoardSide.AWAY, 1, Locator.card_factory.create(test_card_info[0]).GetComponent<Creature>());
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
            StartGame();
#if UNITY_EDITOR
        InputCheatKey();
#endif
    }
    void InputCheatKey()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            TurnManage.OnAddCard?.Invoke(true);
        if (Input.GetKeyDown(KeyCode.Keypad2))
            TurnManage.Inst.EndTurn();
    }
    public void StartGame()
    {
        StartCoroutine(TurnManage.Inst.StartGameCo());
    }

    public void Notification(string message)
    {
        notificationPanel.Show(message);
    }
}