using UnityEngine;
using UnityEngine.UI;
using BackEnd; // Backend ���̺귯���� ����ϱ� ���� �߰�
using LitJson;
using TMPro;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour
{
    public Button But;

    [Header("UI References")]
    public TMP_Text[] rankTexts;      // 10���� ���� �ؽ�Ʈ UI
    public TMP_Text[] nicknameTexts;  // 10���� �г��� �ؽ�Ʈ UI
    public TMP_Text[] scoreTexts;     // 10���� ���� �ؽ�Ʈ UI
    
    public void RankGet()
    {
        var bro1 = Backend.Initialize(true);
        BackendLogin.Instance.CustomLogin("user1", "1234");
        //var bro1 = Backend.Initialize(true);
        string rankUUID = "b2ebe6f0-82da-11ee-966f-814760310d2e"; // �ش� UUID ������ �������ּ���.
        var bro = Backend.URank.User.GetRankList(rankUUID);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("��ŷ ��ȸ �� ������ �߻��߽��ϴ�. : " + bro);
            return;
        }
        Debug.Log("��ŷ ��ȸ�� �����߽��ϴ�. : " + bro);

        int count = 0;
        foreach (JsonData jsonData in bro.FlattenRows())
        {
            if (count >= 10) break;
            Debug.Log(jsonData["nickname"].ToString());
            //rankTexts[count].text = "���� : " + jsonData["rank"].ToString();
            nicknameTexts[count].text = jsonData["nickname"].ToString();
            scoreTexts[count].text = jsonData["score"].ToString();

            count++;
        }
    }


    private void Start()
    {
        RankGet();
        But.onClick.AddListener(EnterMainMenu);
    }

    void EnterMainMenu()
    {
        //StartCoroutine(MapGenerator.Instance.LoadDiffScene("StartMenu"));
        SceneManager.LoadScene("StartMenu");
    }
}
