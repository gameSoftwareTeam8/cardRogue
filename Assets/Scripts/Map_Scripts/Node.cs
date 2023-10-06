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
        spriteRenderer.color = defaultColor; // �ʱ� ���� ����
    }

    public void SetNodeType(NodeType type)
    {
        Type = type;
        UpdateNodeColor();
    }

    public void SetClicked()
    {
        IsClicked = true;

        // ��尡 Ŭ���Ǹ� ����� ���� ������ ǥ��
        UpdateNodeColor();
    }

    private void UpdateNodeColor()
    {
        if (!IsClicked)
        {
            spriteRenderer.color = defaultColor;  // ��尡 Ŭ������ �ʾ����� �⺻ ������ ���
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
            // �ǹ� ���� ��� 
            case NodeType.Boss:
                spriteRenderer.color = Color.white;
                break;


        }
    }

    public void HighlightNode()
    {
        if (!IsClicked) // ��尡 �̹� Ŭ���� ���°� �ƴ϶��
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
