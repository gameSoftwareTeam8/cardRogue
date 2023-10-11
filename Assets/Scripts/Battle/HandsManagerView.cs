using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

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

    CardTransform selectCard;
    bool isMyCardDrag;
    bool onCardArea;
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
        var cardTransform = args.card.GetComponent<CardTransform>();
        if (eCardState == ECardState.Nothing)
            return;
        selectCard = cardTransform;
        EnlargeCard(true, cardTransform);
    }

    public void CardMouseExit(object sender, CardEventArgs args)
    {
        EnlargeCard(false, args.card.GetComponent<CardTransform>());  
    }

    public void CardMouseDown(object sender, CardEventArgs args)
    {
        if (eCardState != ECardState.CanMouseDrag)
            return;
        isMyCardDrag = true;
    }

    public void CardMouseUp(object sender, CardEventArgs args)
    {
        isMyCardDrag = false;
        if (eCardState != ECardState.CanMouseDrag)
            return;
    }

    void CardDrag()
    {
        if (!onCardArea)
        {
            selectCard.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale * cardScale), false);
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
        else if (selectCard != null)
            card.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale * cardScale), false);

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
