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
        string rankUUID = "827ff0f0-773c-11ee-9758-a7f008c5a6b3"; // �ش� UUID ������ �������ּ���.
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
        SceneManager.LoadScene("StartMenu");
    }
}
