using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using BackEnd;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameField;
    public Button loginButton;

    private void Start()
    {
        loginButton.onClick.AddListener(TryLogin);
        var bro = Backend.Initialize(true); // 뒤끝 초기화

        // 뒤끝 초기화에 대한 응답값
        if (bro.IsSuccess())
        {
            Debug.Log("초기화 성공 : " + bro); // 성공일 경우 statusCode 204 Success
        }
        else
        {
            Debug.LogError("초기화 실패 : " + bro); // 실패일 경우 statusCode 400대 에러 발생
        }
    }

    void TryLogin()
    {
        string username = usernameField.text;
        Debug.Log(username);
        BackendLogin.Instance.CustomSignUp(username, "1234");
        BackendLogin.Instance.CustomLogin(username, "1234");
        BackendLogin.Instance.UpdateNickname(username);
        BackendRank.Instance.RankInsert(50);
        SceneManager.LoadScene("StartMenu");

    }
}