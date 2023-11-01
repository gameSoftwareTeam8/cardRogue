using UnityEngine;
using UnityEngine.UI;
using BackEnd; // Backend 라이브러리를 사용하기 위해 추가
using LitJson;
using TMPro;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour
{
    public Button But;

    [Header("UI References")]
    public TMP_Text[] rankTexts;      // 10개의 순위 텍스트 UI
    public TMP_Text[] nicknameTexts;  // 10개의 닉네임 텍스트 UI
    public TMP_Text[] scoreTexts;     // 10개의 점수 텍스트 UI
    
    public void RankGet()
    {
        var bro1 = Backend.Initialize(true);
        BackendLogin.Instance.CustomLogin("user1", "1234");
        //var bro1 = Backend.Initialize(true);
        string rankUUID = "827ff0f0-773c-11ee-9758-a7f008c5a6b3"; // 해당 UUID 값으로 변경해주세요.
        var bro = Backend.URank.User.GetRankList(rankUUID);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("랭킹 조회 중 오류가 발생했습니다. : " + bro);
            return;
        }
        Debug.Log("랭킹 조회에 성공했습니다. : " + bro);

        int count = 0;
        foreach (JsonData jsonData in bro.FlattenRows())
        {
            if (count >= 10) break;
            Debug.Log(jsonData["nickname"].ToString());
            //rankTexts[count].text = "순위 : " + jsonData["rank"].ToString();
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
