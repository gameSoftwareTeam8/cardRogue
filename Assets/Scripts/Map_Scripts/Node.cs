using UnityEngine;
public enum NodeType
{
    None,
    Wall,
    Monster,
    Event,
    Boss
}
public class Node : MonoBehaviour
{
    public NodeType Type { get; private set; }
    public bool IsClicked { get; private set; } = false;

    private SpriteRenderer spriteRenderer;
    private Color defaultColor = Color.black;
    private Color highlightedColor = Color.gray;
    private Color clickedColor = new Color(1, 0.65f, 0);

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = defaultColor; // 초기 색상 설정
    }

    public void SetNodeType(NodeType type)
    {
        Type = type;
        UpdateNodeColor();
    }

    public void SetClicked()
    {
        IsClicked = true;

        // 노드가 클릭되면 노드의 고유 색상을 표시
        UpdateNodeColor();
    }

    private void UpdateNodeColor()
    {
        if (!IsClicked)
        {
            spriteRenderer.color = defaultColor;  // 노드가 클릭되지 않았으면 기본 색상을 사용
            return;
        }

        switch (Type)
        {
            case NodeType.None:
                spriteRenderer.color = clickedColor;
                break;
            case NodeType.Wall:
                spriteRenderer.color = Color.black;
                break;
            case NodeType.Monster:
                spriteRenderer.color = Color.red;
                break;
            case NodeType.Event:
                spriteRenderer.color = Color.blue;
                break;
            // 의미 없는 노드 
            case NodeType.Boss:
                spriteRenderer.color = Color.white;
                break;


        }
    }

    public void HighlightNode()
    {
        if (!IsClicked) // 노드가 이미 클릭된 상태가 아니라면
        {
            spriteRenderer.color = highlightedColor;
        }
    }

    public void ResetColor()
    {
        UpdateNodeColor();
    }

    private void OnMouseDown()
    {
        MapGenerator mapGenerator = FindObjectOfType<MapGenerator>();
        if (mapGenerator != null)
        {
            mapGenerator.NodeClicked(this);
        }


    }
}
