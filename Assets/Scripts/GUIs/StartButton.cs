using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public Canvas canvas;
    public void SceneChange()
    {
        StartCoroutine(zoom_in());
        StartCoroutine(FadeManager.Instance.LoadDiffScene("HeaderBar"));        
    }

    public IEnumerator zoom_in()
    {
        float duration = 1.0f;
        float current_time = 0.0f;
        while (current_time < duration)
        {
            float current_scale = Mathf.Lerp(1.0f, 1.5f, current_time / duration);
            canvas.scaleFactor = current_scale;
            current_time += Time.fixedDeltaTime;
            yield return new WaitForSeconds(0.01f);
        }
    }
}