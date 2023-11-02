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
        var bro = Backend.Initialize(true); // �ڳ� �ʱ�ȭ

        // �ڳ� �ʱ�ȭ�� ���� ���䰪
        if (bro.IsSuccess())
        {
            Debug.Log("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 204 Success
        }
        else
        {
            Debug.LogError("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 400�� ���� �߻�
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