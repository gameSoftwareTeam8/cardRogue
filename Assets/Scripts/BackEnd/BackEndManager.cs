using UnityEngine;
using System.Threading.Tasks;

// 뒤끝 SDK namespace 추가
using BackEnd;

public class BackendManager : MonoBehaviour
{
    void Start()
    {
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

        Test();
    }

    // 동기 함수를 비동기에서 호출하게 해주는 함수(유니티 UI 접근 불가)
    void Test()
    {
        Task.Run(() => {
            BackendLogin.Instance.CustomLogin("user1", "1234"); // 뒤끝 로그인 함수
            
            BackendRank.Instance.RankGet(); // [추가] 랭킹 불러오기 함수

            Debug.Log("테스트를 종료합니다.");
        });
    }
}