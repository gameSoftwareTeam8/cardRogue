using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.IK;
using Random = UnityEngine.Random;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Inst {  get; private set; }
    void Awake() => Inst = this;

    [Header("Develop")]
    [SerializeField][Tooltip("�� ���")] ETurnMode eTurnMode;
    [SerializeField][Tooltip("���� ī�� ����")] int startCardCount;
    [SerializeField] GameObject damagePrefab;
    [SerializeField] public GameObject[] manaPrefab;

    [Header("Properties")]
    public bool isLoading;
    public bool myTurn;
    public int turn { get; private set; } = 0;

    enum ETurnMode {My, Enemy}
    WaitForSeconds delay = new WaitForSeconds(0.1f);
    WaitForSeconds delay07 = new WaitForSeconds(0.7f);
    public static event Action<bool> OnTurnStarted;
    public SpriteRenderer mana_Renderer;
    [SerializeField] public static Sprite[] manas = new Sprite[4];
    private object end_turn_lock = new object();
    private bool is_running = false;
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

    public void add_mana(int cur_mana)
    {

        for(int i=0; i<cur_mana; i++)
        {
            manaPrefab[i].transform.localScale = Vector3.one * 5;
        }

    }

    public void remove_mana(int cur_mana)
    {

        for(int i =0; i<manaPrefab.Length; i++)
        {
            manaPrefab[i].transform.localScale = Vector3.zero;
        }
        add_mana(cur_mana);

    }

    public IEnumerator StartTurnCo()
    {
        IPlayer player = Locator.player;
        isLoading = true;
        if (myTurn)
        {
            BattleManager.Inst.Notification("나의 턴");
            player.recover_mana(player.max_mana);
            add_mana(player.mana);
        }
        else
            BattleManager.Inst.Notification("상대 턴");
        yield return delay07;
        ProcessDrawPhase();
        yield return delay07;
        isLoading = false;
        
        IBoard board = Locator.board;
        for (int i = 0; i < board.size; i++)
            for (int side = 0; side < 2; side++)
                board.get_card((BoardSide)side, i)?.gameObject.SendMessage("on_turn_started", SendMessageOptions.DontRequireReceiver);
        OnTurnStarted?.Invoke(myTurn);
    }

    public void ProcessDrawPhase()
    {
        HandsManager.Inst.AddCard(myTurn);
    }

    void SpawnDamage(int damage, Transform tr)
    {
        var damageComponent = Instantiate(damagePrefab).GetComponent<Damage>();
        damageComponent.SetUpTransform(tr);
        damageComponent.Damaged(damage);
    }

    public async Task battle()
    {
        IBoard board = Locator.board;
        IActionQueue action_queue = Locator.action_queue;

        for (int i = 0; i < board.size; i++)
        {
            List<Action> actions = new();
            for (int side = 0; side < 2; side++)
            {
                Creature card = board.get_card((BoardSide)side, i);
                Creature target = board.get_opposite_card(card);
                if (card != null && target != null) {
                    Vector3 OriginAttackerPos = card.transform.position;
                    Vector3 CardSize = new Vector3(0, 2.5f, 0);
                    Vector3 Middle = side==0 ?(card.transform.position + target.transform.position) / 2 - CardSize : (card.transform.position + target.transform.position) / 2 + CardSize;
                    Vector3 reverseDir = card.transform.position - (target.transform.position - card.transform.position) * 0.2f;

                    actions.Add(() => {
                        DOTween.Sequence()
                            .Append(card.transform.DOMove(reverseDir, 0.3f).SetEase(Ease.InExpo))
                            .AppendInterval(0.2f)
                            .Append(card.transform.DOMove(Middle, 0.01f).SetEase(Ease.Linear))
                            .AppendCallback(() =>
                            {
                                target.attack(card);
                                // SpawnDamage(card.creature_info.power, target.transform);
                            }).Append(card.transform.DOMove((OriginAttackerPos), 0.3f).SetEase(Ease.OutCubic));
                    });
                }
            }
            if (actions.Count > 0) {
                action_queue.enqueue(() => {
                    actions[0]();
                    actions[1]();
                }, 350);
            }
        }
        action_queue.enqueue(()=>{}, 500);
        await action_queue.run();
        
        // for (int i = 0; i < board.size; i++)
        // {
        //     for (int side = 0; side < 2; side++)
        //     {
        //         Creature card = board.get_card((BoardSide)side, i);
        //         if (card != null && card.is_destroyed) {
        //             card.gameObject.SendMessage("on_destroyed", SendMessageOptions.DontRequireReceiver);
        //             board.remove_card(card);
        //         }
        //     }
        // }

        for (int i = 0; i < board.size; i++)
            for (int side = 0; side < 2; side++)
                board.get_card((BoardSide)side, i)?.gameObject.SendMessage("on_turn_ended", SendMessageOptions.DontRequireReceiver);
    }

    public async Task EndTurn()
    {
        lock (end_turn_lock) {
            if (is_running)
                return;
            is_running = true;
        }

        await battle();
        turn++;
        myTurn = !myTurn;
        StartCoroutine(StartTurnCo());

        is_running = false;
        lock (end_turn_lock) { is_running = false; }
    }

    public void try_turn_end()
    {
        EndTurn();
    }
}
