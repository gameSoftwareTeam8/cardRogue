using UnityEngine;
using UnityEngine.UI; // UI 컴포넌트를 사용하기 위해 필요합니다.
using TMPro;
public class PrintScore : MonoBehaviour
{
    public TMP_Text textUI; // Inspector에서 할당할 Text UI 컴포넌트

    void Start()
    {
        // PlayerPrefs에서 "score"라는 키로 저장된 값을 가져옵니다.
        // 만약 "score"라는 키가 없다면, 기본값으로 0을 반환합니다.
        int score = PlayerPrefs.GetInt("score", 0);

        // Text UI 컴포넌트의 text 속성을 업데이트합니다.
        textUI.text = "최종 스코어: " + score.ToString();
    }
}