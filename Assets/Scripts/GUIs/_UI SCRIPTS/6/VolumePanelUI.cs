using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumePanelUI : MonoBehaviour
{
    public GameObject volumePanel, bgmSlider, sfxSlider, quitButton;

    void Start()
    {
        // Get the RectTransform of the volumePanel
        RectTransform volumePanelRectTransform = volumePanel.GetComponent<RectTransform>();


        // Set the size of bgmSlider, sfxSlider, quitButton
        RectTransform bgmSliderRectTransform = bgmSlider.GetComponent<RectTransform>();
        RectTransform sfxSliderRectTransform = sfxSlider.GetComponent<RectTransform>();
        RectTransform quitButtonRectTransform = quitButton.GetComponent<RectTransform>();

        bgmSliderRectTransform.sizeDelta = new Vector2(volumePanelRectTransform.rect.width * 0.8f, volumePanelRectTransform.rect.height / 3f);
        sfxSliderRectTransform.sizeDelta = new Vector2(volumePanelRectTransform.rect.width * 0.8f, volumePanelRectTransform.rect.height / 3f);
        quitButtonRectTransform.sizeDelta = new Vector2(volumePanelRectTransform.rect.width/2, volumePanelRectTransform.rect.height / 5f);

        // Set the anchor presets
        bgmSliderRectTransform.anchorMax = new Vector2(0.5f, 1f);
        bgmSliderRectTransform.anchorMin = new Vector2(0.5f, 1f);
        bgmSliderRectTransform.pivot = new Vector2(0.5f, 1f);

        sfxSliderRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        sfxSliderRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        sfxSliderRectTransform.pivot = new Vector2(0.5f, 0.5f);

        quitButtonRectTransform.anchorMax = new Vector2(0.5f, 0f);
        quitButtonRectTransform.anchorMin = new Vector2(0.5f, 0f);
        quitButtonRectTransform.pivot = new Vector2(0.5f, 0f);

        // Set the position of bgmSlider, sfxSlider, quitButton
        bgmSliderRectTransform.anchoredPosition = new Vector2(0, -50);
        sfxSliderRectTransform.anchoredPosition = new Vector2(0, 0);
        quitButtonRectTransform.anchoredPosition = new Vector2(0, 50);
    }
}






