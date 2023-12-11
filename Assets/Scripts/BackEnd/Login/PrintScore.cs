using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PrintScore : MonoBehaviour
{
    public TMP_Text textUI; // Inspector���� �Ҵ��� Text UI ������Ʈ

    void Start()
    {
        // PlayerPrefs���� "score"��� Ű�� ����� ���� �����ɴϴ�.
        // ���� "score"��� Ű�� ���ٸ�, �⺻������ 0�� ��ȯ�մϴ�.
        int score = PlayerPrefs.GetInt("score", Locator.player.score);

        // Text UI ������Ʈ�� text �Ӽ��� ������Ʈ�մϴ�.
        textUI.text = "Score: " + score.ToString();
    }
}