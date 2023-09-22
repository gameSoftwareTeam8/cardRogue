using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject nodePrefab;

    private static Node[,] savedMap = null;  // 저장된 맵 상태
    private Node[,] nodes;

    private List<Node> wallList = new List<Node>();

    private void Start()
    {
        // 게임 시작후 첫 맵 
        if (savedMap == null)
        {
            // 모든 노드를 Wall로 초기화 
            InitializeMap();
            // 시작 위치를 기반으로 인접한 방이 하나일 경우에만 맵 생성 
            GenerateMazeUsingPrim();

            // 생성된 맵의 상태를 savedMap에 저장
            savedMap = nodes;
        }
        // 게임 진행중 맵 재로드 
        else
        {
            LoadMap();
        }
    }


    // 모든 노드를 Wall로 초기화 한다. 
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
        // 임의의 시작 지점 설정
        // int startX = Random.Range(0, width);
        // int startY = Random.Range(0, height);

        // 시작 노드는 중앙에 위치 
        int startX = width / 2;
        int startY = height / 2;

        // 노드를 None로 설정 
        Node startNode = nodes[startX, startY];
        startNode.SetNodeType(NodeType.None);

        // 노드 근처에 Wall을 모두 wallList안에 집어 넣음 
        AddWallsToList(startNode);

        // wallList에서 랜덤한 노드 하나를 꺼내서 인접한 노드가 하나일 경우에만 Wall 노드를 None으로 전환 
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

        // None 노드 중에서 확률로 Moster 노드나 Event 노드로 바뀜 
        // 아래 코드는 다른 내용을 적용할 예정 
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Node currentNode = nodes[x, y];
                if (currentNode.Type == NodeType.None)
                {
                    float randomValue = Random.value;
                    if (randomValue < 0.1f)  // 10% 
                    {
                        currentNode.SetNodeType(NodeType.Monster);
                    }
                    else if (randomValue < 0.2f)  // 10% 
                    {
                        currentNode.SetNodeType(NodeType.Event);
                    }
                }
            }
        }
    }
    // 노드의 주변 벽 노드를 WallList에 넣는다. 
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

    // 노드 주변의 None 노드를 반환한다. 
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

    private void LoadMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (savedMap[x, y] == null) continue;

                GameObject nodeObj = Instantiate(nodePrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
                Node nodeComponent = nodeObj.GetComponent<Node>();
                nodeComponent.SetNodeType(savedMap[x, y].Type);
            }
        }
    }
}