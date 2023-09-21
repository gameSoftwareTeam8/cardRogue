using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum NodeType
{
    None,
    Empty,  // 비어 있는 방
    Monster,
    Event
}

public class Node : MonoBehaviour
{
    public NodeType Type { get; set; }

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetNodeType(NodeType type)
    {
        Type = type;
        spriteRenderer.color = GetColor();
    }

    private Color GetColor()
    {
        switch (Type)
        {
            case NodeType.None:
                return Color.white;
            case NodeType.Empty:
                return Color.gray;
            case NodeType.Monster:
                return Color.red;
            case NodeType.Event:
                return Color.blue;
            default:
                return Color.white;
        }
    }

    private void OnMouseDown()
    {
        switch (Type)
        {
            case NodeType.None:
            case NodeType.Empty:
                break;
            case NodeType.Monster:
                SceneManager.LoadScene("Temp1");
                break;
            case NodeType.Event:
                SceneManager.LoadScene("Temp2");
                break;
        }
    }
}