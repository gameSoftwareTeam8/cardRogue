using UnityEngine;
using UnityEngine.SceneManagement;

public enum NodeType
{
    None,
    Wall,
    Monster,
    Event,
    Merchant,
    CampFire,
    Boss
}

public class Node : MonoBehaviour
{
    public NodeType Type { get; private set; }
    public bool IsClicked { get; private set; } = false;

    private bool isMouseDown = false;
    private SpriteRenderer spriteRenderer, background_renderer;

    // effect sound 
    private AudioSource audioSource;

    // 각 노드 유형에 대한 스프라이트
    public Sprite noneSprite;
    public Sprite wallSprite;
    public Sprite monsterSprite;
    public Sprite eventSprite;
    public Sprite bossSprite;
    public Sprite MerchantSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        background_renderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // 스프라이트를 기본적으로 숨김
        background_renderer.color = Color.gray;

        SetNodeType(Type);  // 현재 노드의 유형에 따라 스프라이트 설정
        audioSource = GetComponent<AudioSource>();
    }

    public void SetNodeType(NodeType type)
    {
        Type = type;
        UpdateNodeSprite();
    }

    public void SetClicked()
    {
        IsClicked = true;
        UpdateNodeSprite();  // 클릭됐을 때 스프라이트 업데이트
    }

    private void UpdateNodeSprite()
    {
        if (!IsClicked)
        {
            spriteRenderer.color = Color.gray;  // 활성화된 노드를 회색으로 바꿈
        }
        else
        {
            spriteRenderer.color = Color.white; // 클릭된 노드는 원래 색상을 유지
        }

        switch (Type)
        {
            case NodeType.None:
                spriteRenderer.sprite = noneSprite;
                break;
            case NodeType.Wall:
                background_renderer.enabled = false;
                spriteRenderer.sprite = wallSprite;
                break;
            case NodeType.Monster:
                spriteRenderer.sprite = monsterSprite;
                break;
            case NodeType.Event:
                spriteRenderer.sprite = eventSprite;
                break;
            case NodeType.Boss:
                spriteRenderer.sprite = bossSprite;
                break;
            case NodeType.Merchant:
                spriteRenderer.sprite = MerchantSprite;
                break;
        }
    }

    public void HighlightNode()
    {
        IsSpriteEnabled = true;
        background_renderer.color = Color.white;
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        if (!isMouseDown)
            return;
        isMouseDown = false;

        if (SceneManager.GetActiveScene().name != "Map")
        {
            return; 
        }
        if (IsClicked) return;
  
        MapGenerator mapGenerator = FindObjectOfType<MapGenerator>();
        if (mapGenerator != null)
        {
            mapGenerator.NodeClicked(this);
        }
    }
    public bool IsSpriteEnabled
    {
        get { return spriteRenderer.enabled; }
        set {
            spriteRenderer.enabled = value;
            background_renderer.enabled = value;
        }
    }

    private void OnMouseEnter()
    {
        if (audioSource != null && spriteRenderer.enabled == true)
        {
            audioSource.Play();
        }
    }
}
