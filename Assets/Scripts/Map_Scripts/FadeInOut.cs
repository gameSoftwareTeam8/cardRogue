using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public RawImage fadeImage;
    public float fadeDuration = 3.0f;
    private Color currentColor = Color.black;
    private Color targetColor = new Color(0, 0, 0, 0);


    public static FadeInOut Instance { get; private set; }


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
    // Start is called before the first frame update

    void Start()
    {
        //StartCoroutine(MapGenerator.Instance.LoadDiffScene("Map"));
    }

    public IEnumerator FadeIn()
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
}
