using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    public int width = 8;
    public int height = 8;
    public GameObject nodePrefab;

    public RawImage fadeImage;
    public float fadeDuration = 3.0f;
    private Color currentColor = Color.black;
    private Color targetColor = new Color(0, 0, 0, 0);

    public int MonsterNodeCount = 6;
    public int MerchantNodeCount = 3;
    public int EventNodeCount = 3;

    private Node[,] nodes;
    private Node currentNode;
    private List<Node> highlightedNodes = new List<Node>();
    private List<Node> wallList = new List<Node>();
    private List<Node> nodeList = new List<Node>();


    public static MapGenerator Instance { get; private set; }
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayerPrefs.SetInt("Player Score", 0);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(fadeImage.gameObject);
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
            StartCoroutine(FadeIn());
        }
        else
        {
            HideAllSprites();
        }
    }

    public void HideAllSprites()
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
        StartCoroutine(FadeIn());
        //FadeIn();
    }

    private void GenerateMap()
    {
        InitializeMap();
        GenerateMazeUsingPrim();
        //ConversionNode();
        PlaceBossNode();
        AddNodetoList();
        ConverseNode();

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
        wallList.Clear();
    }

    private void ConverseNode()
    {
        while(MonsterNodeCount > 0)
        {
            Node NoneNode = nodeList[Random.Range(0, nodeList.Count)];
            NoneNode.SetNodeType(NodeType.Monster);
            NoneNode.name = "Node_Moster";
            nodeList.Remove(NoneNode);
            MonsterNodeCount--;
        }
        while (MerchantNodeCount > 0)
        {
            Node NoneNode = nodeList[Random.Range(0, nodeList.Count)];
            NoneNode.SetNodeType(NodeType.Merchant);
            NoneNode.name = "Node_Merchant";
            nodeList.Remove(NoneNode);
            MerchantNodeCount--;
        }
        while (EventNodeCount > 0)
        {
            Node NoneNode = nodeList[Random.Range(0, nodeList.Count)];
            NoneNode.SetNodeType(NodeType.Event);
            NoneNode.name = "Node_Event";
            nodeList.Remove(NoneNode);
            EventNodeCount--;
        }
        nodeList.Clear();
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
    private void AddNodetoList()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j=0;j< height; j++)
            {
                if(i == width/2 && j == height/2)
                {
                    continue;
                }
                Node adjacentNode = nodes[i,j];
                if(adjacentNode.Type == NodeType.None)
                {
                    nodeList.Add(adjacentNode);
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
                    //Reset_Map();
                    CountupScore(10);
                    HideAllSprites();
                    StartCoroutine(LoadDiffScene("BattleScene"));
                    //FadeInOut.Instance.FadeOut();
                    //LoadDiffScene("BattleScene");
                    //SceneManager.LoadScene("BattleScene");
                    break;
                case NodeType.Event:
                    CountupScore(5);
                    HideAllSprites();
                    StartCoroutine(LoadDiffScene("Temp1"));
                    //SceneManager.LoadScene("Shelter");
                    //SceneManager.LoadScene("Temp1");
                    break;
                case NodeType.Merchant:
                    CountupScore(1);
                    HideAllSprites();
                    StartCoroutine(LoadDiffScene("Merchant"));
                    //SceneManager.LoadScene("Merchant");
                    break;
                case NodeType.Boss:
                    CountupScore(50);
                    HideAllSprites();
                    StartCoroutine(LoadDiffScene("Temp2"));
                    //SceneManager.LoadScene("Temp2");
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
    
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            currentColor = Color.Lerp(Color.black, Color.clear, elapsedTime / fadeDuration);
            fadeImage.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentColor = Color.clear;
        fadeImage.color = currentColor;
    }
    public IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            currentColor = Color.Lerp(Color.clear, Color.black, elapsedTime / fadeDuration);
            fadeImage.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentColor = Color.black;
        fadeImage.color = currentColor;
    }
    private IEnumerator LoadDiffScene(string SceneName)
    {
        float elapsedTime = 0;
        while(elapsedTime < fadeDuration)
        {
            currentColor = Color.Lerp(Color.clear, Color.black, elapsedTime / fadeDuration);
            fadeImage.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentColor = Color.black;
        fadeImage.color = currentColor;
        SceneManager.LoadScene(SceneName);
        StartCoroutine(FadeIn());
    }
    
}