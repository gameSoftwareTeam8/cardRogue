using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToChangeScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FadeManager.Instance.LoadDiffScene("Map"));
        }
    }
}