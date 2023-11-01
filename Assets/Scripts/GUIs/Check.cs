using UnityEngine;
using UnityEngine.UI;

public class Check : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleButtonClick);
    }

    private void HandleButtonClick()
    {
        Debug.Log("버튼이 클릭되었습니다.");
    }
}