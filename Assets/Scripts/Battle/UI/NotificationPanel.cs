using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class NotificationPanel : MonoBehaviour
{
    [SerializeField] TMP_Text notificationTMP;
    public float scale = 0.7f;

    public void Show(string message)
    {
        notificationTMP.text = message;
        Sequence sequence = DOTween.Sequence()
            .Append(notificationTMP.DOFade(1.0f, 0.0f))
            .Append(transform.DOScale(Vector2.zero, 0.0f))
            .Append(transform.DOScale(scale * Vector2.one, 0.3f).SetEase(Ease.InOutQuad))
            .AppendInterval(0.9f)
            .Append(notificationTMP.DOFade(0.0f, 0.3f).SetEase(Ease.InOutQuad));
    }

    private void Start() => ScaleZero();

    [ContextMenu("ScaleOne")]
    void ScaleOne() => transform.localScale = Vector3.one;

    [ContextMenu("ScaleZero")]
    void ScaleZero() => transform.localScale = Vector3.zero;
}
