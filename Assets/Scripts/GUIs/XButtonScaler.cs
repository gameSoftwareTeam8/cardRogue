using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class XButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject XButton;
    public Vector3 normalScale = new Vector3(1, 1, 1); // 기본 크기
    public Vector3 enlargedScale = new Vector3(2f, 2f, 2f); // 확대될 크기

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = enlargedScale; // 마우스가 올라갔을 때 확대
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = normalScale; // 마우스가 벗어났을 때 원래 크기로 복귀
        Canvas.ForceUpdateCanvases();
    }
}