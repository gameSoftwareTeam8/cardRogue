using UnityEngine;
using System.Threading.Tasks;

// �ڳ� SDK namespace �߰�
using BackEnd;

public class BackendManager : MonoBehaviour
{
    void Start()
    {
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

        Test();
    }

    // ���� �Լ��� �񵿱⿡�� ȣ���ϰ� ���ִ� �Լ�(����Ƽ UI ���� �Ұ�)
    void Test()
    {
        Task.Run(() => {
            BackendLogin.Instance.CustomLogin("user1", "1234"); // �ڳ� �α��� �Լ�
            
            BackendRank.Instance.RankGet(); // [�߰�] ��ŷ �ҷ����� �Լ�

            Debug.Log("�׽�Ʈ�� �����մϴ�.");
        });
    }
}