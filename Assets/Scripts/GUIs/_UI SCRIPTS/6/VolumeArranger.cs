using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{


    public Button button1;
    public Button button2;
    public Slider slider;


    void Start()
    {
        // 버튼1, 슬라이더, 버튼2 순서로 배치
        RectTransform rtButton1 = button1.GetComponent<RectTransform>();
        RectTransform rtButton2 = button2.GetComponent<RectTransform>();
        RectTransform rtSlider = slider.GetComponent<RectTransform>();

        // 슬라이더 위치 설정
        rtSlider.anchorMin = new Vector2(0.5f, 0.5f);
        rtSlider.anchorMax = new Vector2(0.5f, 0.5f);
        rtSlider.pivot = new Vector2(0.5f, 0.5f);
        rtSlider.anchoredPosition = new Vector2(0, 0);

        // 버튼1 위치 설정
        rtButton1.anchorMin = new Vector2(0, 0.5f);
        rtButton1.anchorMax = new Vector2(0, 0.5f);
        rtButton1.pivot = new Vector2(0, 0.5f);
        rtButton1.anchoredPosition = new Vector2();

        // 버튼2 위치 설정
        rtButton2.anchorMin = new Vector2(1, 0.5f);
        rtButton2.anchorMax = new Vector2(1, 0.5f);
        rtButton2.pivot = new Vector2(1, 0.5f);
        rtButton2.anchoredPosition = new Vector2(0, 0);
    }
}