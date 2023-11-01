using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image targetImage;
    public string normalColorHex = "#FFFFFF";
    public string hoverColorHex = "#FF0000";
    private Color normalColor;
    private Color hoverColor;

    public AudioClip buttonHoverSound;  // Button hover sound clip
    private AudioSource audioSource;  // Audio source component

    void Start()
    {
        if (targetImage == null)
        {
            targetImage = GetComponent<Image>();
        }

        // Hex 코드를 Color로 변환
        ColorUtility.TryParseHtmlString(normalColorHex, out normalColor);
        ColorUtility.TryParseHtmlString(hoverColorHex, out hoverColor);

        // Get the audio source component
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = 0.25f;

        if (audioSource == null)  // If no audio source component is attached to this game object, create one
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetImage != null)
        {
            targetImage.color = hoverColor;
        }

        // Play the button hover sound
        if (buttonHoverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonHoverSound);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetImage != null)
        {
            targetImage.color = normalColor;
        }
    }
}


