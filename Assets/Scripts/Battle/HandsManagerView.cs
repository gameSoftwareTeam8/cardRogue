using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;


public class HandsManagerView : MonoBehaviour
{
    public static HandsManagerView Inst {  get; private set; }
    void Awake() => Inst = this;

    [SerializeField] List<CardTransform> myCards;
    [SerializeField] Transform cardSpawnPoint;
    [SerializeField] Transform myCardLeft;
    [SerializeField] Transform myCardRight;
    [SerializeField] ECardState eCardState;
    [SerializeField] float cardScale = 1.0f;

    GameObject enlargedInstance = null;
    CardTransform selectCard;
    bool isMyCardDrag = false;
    bool onCardArea;
    bool isCardUsed = false;
    enum ECardState { Nothing, CanMouseOver, CanMouseDrag};

    private void FixedUpdate()
    {
        if (isMyCardDrag)
            CardDrag();

        DetectCardArea();
        SetECardState();
    }

    void OnCardAdded((bool myTurn, Card card) args)
    {
        var cardView = args.card.GetComponent<CardView>();
        cardView.transform.position = cardSpawnPoint.position;
        cardView.on_mouse_up += CardMouseUp;
        cardView.on_mouse_down += CardMouseDown;
        cardView.on_mouse_over += CardMouseOver;
        cardView.on_mouse_exit += CardMouseExit;
        cardView.AddComponent<CardRenderingOrderer>();
        cardView.AddComponent<CardTransform>();
        cardView.show();

        var cardTransform = args.card.GetComponent<CardTransform>();
        if(args.myTurn)
            myCards.Add(cardTransform);

        SetOriginOrder();
        CardAlignment();
    }

    void OnCardRemoved(Card card)
    {
        Destroy(card.gameObject);
        myCards.Remove(card.GetComponent<CardTransform>());
        SetOriginOrder();
        CardAlignment();
    }

    void SetOriginOrder()
    {
        int count = myCards.Count;
        for(int i = 0;i < count;i++)
        {
            var targetCard = myCards[i];
            targetCard?.GetComponent<CardRenderingOrderer>().SetOriginOrder(i);
        }
    }

    void CardAlignment()
    {
        List<PRS> originCardPRSs = new List<PRS>();
        originCardPRSs = RoundAlignment(myCardLeft, myCardRight, myCards.Count, Vector3.one * 1.9f);
        var targetCards = myCards;
        for (int i = 0; i < targetCards.Count; i++)
        {
            var targetCard = targetCards[i];

            targetCard.originPRS = originCardPRSs[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 0.7f * cardScale);
        }
    }

    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount);

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                float interval = 1f / (objCount - 1);
                for(int i=0; i< objCount; i++)
                {
                    objLerps[i] = interval * i;
                }
                break;

        }

        for(int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Utils.QI;
            if (objCount >= 4)
            {
                float curve = Mathf.Sqrt(Mathf.Pow(0.5f, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }
            results.Add(new PRS(targetPos, targetRot, scale * cardScale));
        }
        return results;
    }

    #region MyCard
    public void CardMouseOver(object sender, CardEventArgs args)
    {
        if (isCardUsed) {
            isCardUsed = false;
            return;
        }
        if (enlargedInstance != null)
            return;

        var cardTransform = args.card.GetComponent<CardTransform>();
        if (eCardState == ECardState.Nothing || isMyCardDrag)
            return;

        enlargedInstance = Instantiate(args.card.gameObject);
        foreach (var component in enlargedInstance.GetComponents<Component>())
            if (!(component is CardTransform || component is Transform || component is CardRenderingOrderer))
                Destroy(component);
        enlargedInstance.GetComponent<CardView>().show();

        selectCard = cardTransform;
        EnlargeCard(true, enlargedInstance.GetComponent<CardTransform>());
        selectCard.GetComponent<CardView>().hide();
    }

    public void CardMouseExit(object sender, CardEventArgs args)
    {
        if (enlargedInstance != null)
            Destroy(enlargedInstance);
        if (selectCard != null)
            selectCard.GetComponent<CardView>().show();
        if (!isMyCardDrag)
            selectCard = null;
        // EnlargeCard(false, args.card.GetComponent<CardTransform>());  
    }

    public void CardMouseDown(object sender, CardEventArgs args)
    {
        if (eCardState != ECardState.CanMouseDrag)
            return;
        isMyCardDrag = true;
    }

    public void CardMouseUp(object sender, CardEventArgs args)
    {
        isCardUsed = true;
        if (enlargedInstance != null)
            Destroy(enlargedInstance);
        if (selectCard != null) {
            selectCard.GetComponent<CardView>().show();
            EnlargeCard(false, selectCard);
            selectCard = null;
        }
        if (!isMyCardDrag)
            return;
        isMyCardDrag = false;

        var layer = LayerMask.GetMask("CardZone");
        RaycastHit2D hit = Physics2D.Raycast(Utils.MousePos, Vector2.zero, Mathf.Infinity, layer);
        if (use_nontarget(args.card) || onCardArea || !hit)
            return;
                
        string zone_name = hit.transform.name;
        BoardSide side = zone_name[0] == 'H' ? BoardSide.HOME : BoardSide.AWAY;
        int idx = zone_name[^1] - '0';
        use_card(side, idx, args.card);

        if (eCardState != ECardState.CanMouseDrag)
            return;
    }

    private bool use_nontarget(Card card)
    {
        CardEffect card_effect = card.GetComponent<CardEffect>();
        if (card_effect is not TargetingMagicEffect && card_effect is MagicEffect) {
            ((MagicEffect)card_effect).on_used(BoardSide.HOME);
            HandsManager.Inst.RemoveCard(card);
            return true;
        }
        return false;
    }

    private void use_card(BoardSide side, int idx, Card card)
    {
        IBoard board = Locator.board;
        CardEffect card_effect = card.GetComponent<CardEffect>();
        Card target = board.get_card(side, idx);
        if (card is Creature && board.get_card(side, idx) == null) {
            var card_factory = Locator.card_factory;
            Creature creature = card_factory.create(card.info).GetComponent<Creature>();
            creature.transform.localScale = Vector2.one * 1.5f * cardScale;
            board.add_card(side, idx, creature);
            HandsManager.Inst.RemoveCard(card);
        }
        else if (card_effect is TargetingMagicEffect && target != null) {
            ((TargetingMagicEffect)card_effect).on_used_to_target((Creature)target);
            HandsManager.Inst.RemoveCard(card);
        }
    }

    void CardDrag()
    {
        if (!onCardArea && selectCard != null)
        {
            selectCard.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale), false);
        }
    }

    void DetectCardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);
        int layer = LayerMask.NameToLayer("CardArea");
        onCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
    }

    void EnlargeCard(bool isEnlarge, CardTransform card)
    {
        if (isEnlarge)
        {
            Vector3 enlargePos = new Vector3(card.originPRS.pos.x, -6f, -10f);
            card.MoveTransform(new PRS(enlargePos, Utils.QI, Vector3.one * 2.8f * cardScale), false);
        }
        else
            card.MoveTransform(card.originPRS, false);

        card.GetComponent<CardRenderingOrderer>().SetMostFrontOrder(isEnlarge);
    }

    void SetECardState()
    {
        if (TurnManager.Inst.isLoading)
        {
            eCardState = ECardState.Nothing;
        }
        else if (!TurnManager.Inst.myTurn)
        {
            eCardState = ECardState.CanMouseOver;
        }
        else if (TurnManager.Inst.myTurn)
        {
            eCardState = ECardState.CanMouseDrag;
        }
    }
    #endregion
}
