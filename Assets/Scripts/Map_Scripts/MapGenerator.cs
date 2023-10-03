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
    private List<Node> highlightedNodes = new List<Node>(); // 활성화된 노드를 추적하기 위한 리스트
    private List<Node> wallList = new List<Node>(); // Prim 알고리즘을 위한 wallList

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
        // Map Scene일 때에만 Sprite를 보여줌
        if (scene.name == "Map")
        {
            ShowAllSprites();
        }
        // 다른 씬일 경우 Sprite를 숨김 
        else
        {
            HideAllSprites();
        }
    }

    // 씬 전환할 때 모든 Sprite를 숨김 
    private void HideAllSprites()
    {
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.enabled = false;
        }
    }
     
    // 보스를 클리어시 호출될 함수로 
    // 모든 노드를 삭제하고 다시 생성하게 됨 
    public void ReloadCurrentScene()
    {
        DestroyPersistentObject(); // 모든 노드 삭제 
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // 현재 씬 재로드 
        SceneManager.LoadScene(currentSceneIndex); 
    }
    private void DestroyPersistentObject()
    {
        if (this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
    }
    // 모든 Sprite를 보여줌 
    private void ShowAllSprites()
    {
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.enabled = true;
        }
    }

    private void Start()
    {
        // 1. 모든 노드를 Wall로 초기화
        InitializeMap(); 
        // 2. Prim 알고리즘을 이용해서 None 노드가 
        GenerateMazeUsingPrim();
        // 3. None 노드 중에서 확률로 Monster 노드 , Event 노드로 바뀜 
        ConversionNode();
        // 4. 보스를 추가 
        PlaceBossNode();
        // 5. 현재 노드에서 주변 노드를 회색칠 -> 이미지 추가하면서 다른 방식으로 바꿀 예정 
        currentNode = nodes[width / 2, height / 2];
        HighlightAdjacentNodes(currentNode);
    }

    // 위 함수를 제외한 대부분의 함수는 디자인 처리인데 나중에 에셋을 이용할 예정이므로
    // 높은 확률로 수정하거나 삭제될 예정 

    private void InitializeMap()
    {
        nodes = new Node[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject nodeObj = Instantiate(nodePrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
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
                    if (randomValue < 0.1f)  // 10% 확률로 Monster 노드로 변경
                    {
                        currentNode.SetNodeType(NodeType.Monster);
                    }
                    else if (randomValue < 0.2f)  // 다음 10% 확률 (즉, 전체의 10%)로 Event 노드로 변경
                    {
                        currentNode.SetNodeType(NodeType.Event);
                    }
                }
            }
        }
    }
    // 알고리즘 상 문제가 있으나 
    // 문제가 잘 안생기기에 패스 
    private void PlaceBossNode()
    {
        List<Vector2Int> bossPositions = new List<Vector2Int>()
        {
        new Vector2Int(0, 0),           // 왼쪽 아래
        new Vector2Int(0, height - 1),  // 왼쪽 위
        new Vector2Int(width - 1, 0),   // 오른쪽 아래
        new Vector2Int(width - 1, height - 1)  // 오른쪽 위
        };

        Vector2Int bossPosition = bossPositions[Random.Range(0, bossPositions.Count)];
        nodes[bossPosition.x, bossPosition.y].SetNodeType(NodeType.Boss);
    }
    private void AddWallsToList(Node node)
    {
        int x = (int)node.transform.position.x;
        int y = (int)node.transform.position.y;

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
        int x = (int)node.transform.position.x;
        int y = (int)node.transform.position.y;

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

            // 모든 Sprite를 숨김
            // 클릭된 노드의 타입에 따라 씬을 전환
            switch (clickedNode.Type)
            {
                case NodeType.Monster:
                    HideAllSprites(); 
                    SceneManager.LoadScene("Temp1");
                    break;
                case NodeType.Event:
                    HideAllSprites(); 
                    SceneManager.LoadScene("Temp1");
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
        int dx = (int)a.transform.position.x - (int)b.transform.position.x;
        int dy = (int)a.transform.position.y - (int)b.transform.position.y;

        return (dx == 1 && dy == 0) || (dx == -1 && dy == 0) || (dx == 0 && dy == 1) || (dx == 0 && dy == -1);
    }

    private List<Node> GetAdjacentRooms(Node node)
    {
        List<Node> rooms = new List<Node>();

        int x = (int)node.transform.position.x;
        int y = (int)node.transform.position.y;

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
