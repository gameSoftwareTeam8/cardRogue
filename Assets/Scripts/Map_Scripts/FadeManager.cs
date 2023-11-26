using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{

    public RawImage fadeImage;
    public float fadeDuration = 3.0f;
    private Color currentColor = Color.black;
    private Color targetColor = new Color(0, 0, 0, 0);

    // Start is called before the first frame update
    public static FadeManager Instance { get; private set; }
    

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            currentColor = Color.Lerp(Color.black, Color.clear, elapsedTime / fadeDuration);
            fadeImage.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentColor = Color.clear;
        fadeImage.color = currentColor;
    }
    public IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            currentColor = Color.Lerp(Color.clear, Color.black, elapsedTime / fadeDuration);
            fadeImage.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentColor = Color.black;
        fadeImage.color = currentColor;
    }
    public IEnumerator LoadDiffScene(string SceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            currentColor = Color.Lerp(Color.clear, Color.black, elapsedTime / fadeDuration);
            fadeImage.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentColor = Color.black;
        fadeImage.color = currentColor;
        SceneManager.LoadScene(SceneName, mode);
        StartCoroutine(FadeIn());
    }


}
