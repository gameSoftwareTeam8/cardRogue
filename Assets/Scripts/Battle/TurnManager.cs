using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
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


    enum ETurnMode {My, Enemy}
    WaitForSeconds delay = new WaitForSeconds(0.1f);
    WaitForSeconds delay07 = new WaitForSeconds(0.7f);

    public static Action<bool> OnAddCard;
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
        for(int i=0; i<startCardCount; i++)
        {
            yield return delay;
            OnAddCard?.Invoke(myTurn);
        }
        StartCoroutine(StartTurnCo());
    }

    public IEnumerator StartTurnCo()
    {
        isLoading = true;
        if (myTurn)
            GameManager.Inst.Notification("���� ��");
        else
            GameManager.Inst.Notification("��� ��");
        yield return delay07;
        OnAddCard?.Invoke(myTurn);
        yield return delay07;
        isLoading = false;
    }

    public void EndTurn()
    {
        myTurn = !myTurn;
        StartCoroutine(StartTurnCo());
    }
}
