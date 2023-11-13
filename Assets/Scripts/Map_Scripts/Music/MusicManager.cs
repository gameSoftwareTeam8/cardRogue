using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private AudioSource audioSource;
    public float FadeInDuration = 1f;
    public float FadeOutDuration = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Map")
        {
            FadeInMusic(FadeInDuration);
        }
        else
        {
            FadeOutMusic(FadeOutDuration); 
        }
    }

    public void FadeInMusic(float duration)
    {
        StartCoroutine(FadeMusicIn(duration));
    }

    public void FadeOutMusic(float duration)
    {
        StartCoroutine(FadeMusicOut(duration));
    }

    private IEnumerator FadeMusicIn(float duration)
    {
        float currentTime = 0;
        float startVolume = audioSource.volume;

        audioSource.Play();

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 1f, currentTime / duration);
            yield return null;
        }
    }

    private IEnumerator FadeMusicOut(float duration)
    {
        float currentTime = 0;
        float startVolume = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0, currentTime / duration);
            yield return null;
        }

        audioSource.Pause();
    }
}
