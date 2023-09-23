using UnityEngine;
using UnityEngine.SceneManagement;

public enum NodeType
{
    None,
    Wall,
    Monster,
    Event
}

public class Node : MonoBehaviour
{
    public NodeType Type { get; private set; }
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetNodeType(NodeType type)
    {
        Type = type;
        switch (type)
        {
            case NodeType.None:
                spriteRenderer.color = Color.white;
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
        }
    }

    private void OnMouseUpAsButton()
    {
        switch (Type)
        {
            case NodeType.Monster:
                SceneManager.LoadScene("Temp1");
                break;
            case NodeType.Event:
                SceneManager.LoadScene("Temp2");
                break;
        }
    }
}