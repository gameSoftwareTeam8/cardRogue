using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] TMP_Text damageTMP;
    Transform tr;

    public void SetUpTransform(Transform tr)
    {
        this.tr = tr;
    }
    // Update is called once per frame
    void Update()
    {
        if(tr != null)
        {
            transform.position = tr.position;
        }
    }

    public void Damaged(int damage)
    {
        if (damage <= 0)
            return;

        damageTMP.text = $"-{damage}";

        DG.Tweening.Sequence sequence = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one * 1.8f, 0.5f).SetEase(Ease.InOutBack))
            .AppendInterval(1.2f)
            .Append(transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutBack))
            .OnComplete(()=> Destroy(gameObject));
    }
}
