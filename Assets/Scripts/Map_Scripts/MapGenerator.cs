using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MapGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject nodePrefab;

    private Node[,] nodes;
    private Node currentNode;
    private List<Node> highlightedNodes = new List<Node>(); // Ȱ��ȭ�� ��带 �����ϱ� ���� ����Ʈ
    private List<Node> wallList = new List<Node>(); // Prim �˰������� ���� wallList

    public static MapGenerator Instance { get; private set; }


    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
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
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Map Scene�� ������ Sprite�� ������
        if (scene.name == "Map")
        {
            ShowAllSprites();
        }
        // �ٸ� ���� ��� Sprite�� ���� 
        else
        {
            HideAllSprites();
        }
    }

    // �� ��ȯ�� �� ��� Sprite�� ���� 
    private void HideAllSprites()
    {
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.enabled = false;
        }
    }
     
    // ������ Ŭ����� ȣ��� �Լ��� 
    // ��� ��带 �����ϰ� �ٽ� �����ϰ� �� 
    public void ReloadCurrentScene()
    {
        DestroyPersistentObject(); // ��� ��� ���� 
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // ���� �� ��ε� 
        SceneManager.LoadScene(currentSceneIndex); 
    }
    private void DestroyPersistentObject()
    {
        if (this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
    }
    // ��� Sprite�� ������ 
    private void ShowAllSprites()
    {
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.enabled = true;
        }
    }

    private void Start()
    {
        // 1. ��� ��带 Wall�� �ʱ�ȭ
        InitializeMap(); 
        // 2. Prim �˰������� �̿��ؼ� None ��尡 
        GenerateMazeUsingPrim();
        // 3. None ��� �߿��� Ȯ���� Monster ��� , Event ���� �ٲ� 
        ConversionNode();
        // 4. ������ �߰� 
        PlaceBossNode();
        // 5. ���� ��忡�� �ֺ� ��带 ȸ��ĥ -> �̹��� �߰��ϸ鼭 �ٸ� ������� �ٲ� ���� 
        currentNode = nodes[width / 2, height / 2];
        HighlightAdjacentNodes(currentNode);
    }

    // �� �Լ��� ������ ��κ��� �Լ��� ������ ó���ε� ���߿� ������ �̿��� �����̹Ƿ�
    // ���� Ȯ���� �����ϰų� ������ ���� 

    private void InitializeMap()
    {
        nodes = new Node[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject nodeObj = Instantiate(nodePrefab);
                nodeObj.transform.parent = transform;
                nodeObj.transform.localPosition = new Vector3(x, y, 0);
                Node nodeComponent = nodeObj.GetComponent<Node>();
                nodeComponent.SetNodeType(NodeType.Wall);
                nodes[x, y] = nodeComponent;
            }
        }
    }

    private void GenerateMazeUsingPrim()
    {
        int startX = width / 2;
        int startY = height / 2;

        Node startNode = nodes[startX, startY];
        startNode.SetNodeType(NodeType.None);
        AddWallsToList(startNode);

        while (wallList.Count > 0)
        {
            Node currentWall = wallList[Random.Range(0, wallList.Count)];
            List<Node> adjacentRooms = GetAdjacentRooms(currentWall);
            if (adjacentRooms.Count == 1)
            {
                currentWall.SetNodeType(NodeType.None);
                AddWallsToList(currentWall);
            }

            wallList.Remove(currentWall);
        }
    }
    private void ConversionNode()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Node currentNode = nodes[x, y];
                if (currentNode.Type == NodeType.None)
                {
                    float randomValue = Random.value;
                    if (randomValue < 0.1f)  // 10% Ȯ���� Monster ���� ����
                    {
                        currentNode.SetNodeType(NodeType.Monster);
                    }
                    else if (randomValue < 0.2f)  // ���� 10% Ȯ�� (��, ��ü�� 10%)�� Event ���� ����
                    {
                        currentNode.SetNodeType(NodeType.Event);
                    }
                }
            }
        }
    }
    // �˰����� �� ������ ������ 
    // ������ �� �Ȼ���⿡ �н� 
    private void PlaceBossNode()
    {
        List<Vector2Int> bossPositions = new List<Vector2Int>()
        {
        new Vector2Int(0, 0),           // ���� �Ʒ�
        new Vector2Int(0, height - 1),  // ���� ��
        new Vector2Int(width - 1, 0),   // ������ �Ʒ�
        new Vector2Int(width - 1, height - 1)  // ������ ��
        };

        Vector2Int bossPosition = bossPositions[Random.Range(0, bossPositions.Count)];
        nodes[bossPosition.x, bossPosition.y].SetNodeType(NodeType.Boss);
    }
    private void AddWallsToList(Node node)
    {
        int x = (int)node.transform.localPosition.x;
        int y = (int)node.transform.localPosition.y;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 ^ j == 0)
                {
                    if (x + i >= 0 && x + i < width && y + j >= 0 && y + j < height)
                    {
                        Node adjacentNode = nodes[x + i, y + j];
                        if (adjacentNode.Type == NodeType.Wall && !wallList.Contains(adjacentNode))
                        {
                            wallList.Add(adjacentNode);
                        }
                    }
                }
            }
        }
    }

    private void HighlightAdjacentNodes(Node node)
    {
        int x = (int)node.transform.localPosition.x;
        int y = (int)node.transform.localPosition.y;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 ^ j == 0)
                {
                    if (x + i >= 0 && x + i < width && y + j >= 0 && y + j < height)
                    {
                        Node adjacentNode = nodes[x + i, y + j];
                        if (adjacentNode.Type != NodeType.Wall && !adjacentNode.IsClicked)
                        {
                            adjacentNode.HighlightNode();
                            highlightedNodes.Add(adjacentNode);
                        }
                    }
                }
            }
        }
    }

    public void NodeClicked(Node clickedNode)
    {
        if (highlightedNodes.Contains(clickedNode))
        {
            currentNode = clickedNode;
            currentNode.SetClicked();
            HighlightAdjacentNodes(currentNode);
            Debug.Log(clickedNode.Type);

            // ��� Sprite�� ����
            // Ŭ���� ����� Ÿ�Կ� ���� ���� ��ȯ
            switch (clickedNode.Type)
            {
                case NodeType.Monster:
                    HideAllSprites(); 
                    SceneManager.LoadScene("Temp1");
                    break;
                case NodeType.Event:
                    HideAllSprites(); 
                    SceneManager.LoadScene("Merchant");
                    break;
                case NodeType.Boss:
                    HideAllSprites();
                    SceneManager.LoadScene("Temp2");
                    break;
            }
        }
    }


    private bool IsAdjacent(Node a, Node b)
    {
        int dx = (int)a.transform.localPosition.x - (int)b.transform.localPosition.x;
        int dy = (int)a.transform.localPosition.y - (int)b.transform.localPosition.y;

        return (dx == 1 && dy == 0) || (dx == -1 && dy == 0) || (dx == 0 && dy == 1) || (dx == 0 && dy == -1);
    }

    private List<Node> GetAdjacentRooms(Node node)
    {
        List<Node> rooms = new List<Node>();

        int x = (int)node.transform.localPosition.x;
        int y = (int)node.transform.localPosition.y;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 ^ j == 0)
                {
                    if (x + i >= 0 && x + i < width && y + j >= 0 && y + j < height)
                    {
                        Node adjacentNode = nodes[x + i, y + j];
                        if (adjacentNode.Type == NodeType.None)
                        {
                            rooms.Add(adjacentNode);
                        }
                    }
                }
            }
        }

        return rooms;
    }
}
