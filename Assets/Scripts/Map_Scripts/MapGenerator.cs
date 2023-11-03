using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGenerator : MonoBehaviour
{
    public int width = 8;
    public int height = 8;
    public GameObject nodePrefab;

    private Node[,] nodes;
    private Node currentNode;
    private List<Node> highlightedNodes = new List<Node>();
    private List<Node> wallList = new List<Node>();

    public static MapGenerator Instance { get; private set; }
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayerPrefs.SetInt("Player Score", 0);
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
        if (scene.name == "Map" && nodes != null)
        {
            ShowActiveSprites();
        }
        else
        {
            HideAllSprites();
        }
    }

    private void HideAllSprites()
    {
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.enabled = false;
        }
    }
    public void Reset_Map()
    {
        if (nodes != null && nodes.Length > 0)
        {
            foreach (Node node in nodes)
            {
                Destroy(node.gameObject);
            }
        }
        nodes = null; 

        GenerateMap();
    }
    public void ReloadCurrentScene()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                if (nodes[x, y] != null)
                    Destroy(nodes[x, y]);
        GenerateMap();
    }

    public void ShowActiveSprites()
    {
        /*
        if(nodes == null)
        {
            return;
        }
        */
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Node node = nodes[x, y];
                if (node.IsClicked || highlightedNodes.Contains(node))
                {
                    node.IsSpriteEnabled = true;
                }
                else
                {
                    node.IsSpriteEnabled = false;
                }
            }
        }
        
    }

    private void Start() { 

        GenerateMap();
    }

    private void GenerateMap()
    {
        InitializeMap();
        GenerateMazeUsingPrim();
        ConversionNode();
        PlaceBossNode();
        currentNode = nodes[width / 2, height / 2]; 
                 
        HighlightAdjacentNodes(currentNode);
        currentNode.SetNodeType(NodeType.None);     
        currentNode.SetClicked();
        currentNode.HighlightNode();
    }

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
                nodeObj.name = "Node_Wall";
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
                    if (randomValue < 0.2f)  // 10% Ȯ���� Monster ���� ����
                    {
                        currentNode.SetNodeType(NodeType.Monster);
                        currentNode.name = "Node_Moster";
                    }
                    else if (randomValue < 0.4f)  // ���� 10% Ȯ�� (��, ��ü�� 10%)�� Event ���� ����
                    {
                        currentNode.SetNodeType(NodeType.Event);
                        currentNode.name = "Node_Event";
                    }
                    else if (randomValue < 0.5f)
                    {
                        currentNode.SetNodeType(NodeType.Merchant);
                        currentNode.name = "Node_Merchant";
                    }
                }
            }
        }
    }
    // modified Boss generator Algorithm
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
        Node potentialBossNode;
        do
        {
            bossPosition = bossPositions[Random.Range(0, bossPositions.Count)];
            potentialBossNode = nodes[bossPosition.x, bossPosition.y];
        }
        while (potentialBossNode.Type == NodeType.Wall);  

        potentialBossNode.SetNodeType(NodeType.Boss);
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
    void CountupScore(int score)
    {
        int currentScore = PlayerPrefs.GetInt("Player Score", 0);
        currentScore += score;
        PlayerPrefs.SetInt("Player Score", currentScore);
        PlayerPrefs.Save();
    }
    public void NodeClicked(Node clickedNode)
    {
        if (highlightedNodes.Contains(clickedNode))
        {
            currentNode = clickedNode;
            currentNode.SetClicked();
            HighlightAdjacentNodes(currentNode);

            switch (clickedNode.Type)
            {
                case NodeType.Monster:
                    Reset_Map();
                    CountupScore(10);
                    HideAllSprites();
                    SceneManager.LoadScene("BattleScene");
                    break;
                case NodeType.Event:
                    CountupScore(5);
                    HideAllSprites();
                    SceneManager.LoadScene("Shelter");
                    break;
                case NodeType.Merchant:
                    CountupScore(1);
                    HideAllSprites();
                    SceneManager.LoadScene("Merchant");
                    break;
                case NodeType.Boss:
                    CountupScore(50);
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