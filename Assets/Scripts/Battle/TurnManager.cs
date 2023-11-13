using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Inst {  get; private set; }
    void Awake() => Inst = this;

    [Header("Develop")]
    [SerializeField][Tooltip("�� ���")] ETurnMode eTurnMode;
    [SerializeField][Tooltip("���� ī�� ����")] int startCardCount;

    [Header("Properties")]
    public bool isLoading;
    public bool myTurn;
    public int turn { get; private set; } = 0;
    int MaxMana = 0;
    public static int CurMana;

    enum ETurnMode {My, Enemy}
    WaitForSeconds delay = new WaitForSeconds(0.1f);
    WaitForSeconds delay07 = new WaitForSeconds(0.7f);
    public static event Action<bool> OnTurnStarted;

    void GameSetup()
    {
        if(eTurnMode == ETurnMode.My)
            myTurn = true;
        else if ( eTurnMode == ETurnMode.Enemy)
            myTurn = false;
    }

    public IEnumerator StartGameCo()
    {
        GameSetup();
        isLoading = true;
        for(int i = 0; i < startCardCount; i++)
        {
            yield return delay;
            HandsManager.Inst.AddCard(myTurn);
        }
        StartCoroutine(StartTurnCo());
    }

    public IEnumerator StartTurnCo()
    {
        isLoading = true;
        if (myTurn)
        {
            BattleManager.Inst.Notification("나의 턴");
            if (MaxMana < 10)
                MaxMana++;
            CurMana = MaxMana;
        }
        else
            BattleManager.Inst.Notification("상대 턴");
        yield return delay07;
        ProcessDrawPhase();
        yield return delay07;
        isLoading = false;
        OnTurnStarted?.Invoke(myTurn);
    }

    public void ProcessDrawPhase()
    {
        HandsManager.Inst.AddCard(myTurn);
    }

    public void battle()
    {
        IBoard board = Locator.board;

        for (int i = 0; i < board.size; i++)
        {
            for (int side = 0; side < 2; side++)
            {
                Creature card = board.get_card((BoardSide)side, i);
                Creature target = board.get_opposite_card(card);
                if (card != null && target != null) {
                    Vector3 OriginAttackerPos = card.transform.position;
                    DG.Tweening.Sequence sequence = DOTween.Sequence()
                        .Append(card.transform.DOMove((card.transform.position + target.transform.position) / 2, 0.4f)).SetEase(Ease.InSine)
                        .AppendCallback(() =>
                        {
                            card.attack(target);
                        })
                        .Append(card.transform.DOMove((OriginAttackerPos), 0.4f)).SetEase(Ease.OutSine);
                }
            }
        }
    }

    public void EndTurn()
    {
        battle();
        turn++;
        myTurn = !myTurn;
        StartCoroutine(StartTurnCo());
    }
}
