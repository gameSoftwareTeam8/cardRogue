using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject nodePrefab;

    private static Node[,] savedMap = null;  // ����� �� ����
    private Node[,] nodes;

    private List<Node> wallList = new List<Node>();

    private void Start()
    {
        // ���� ������ ù �� 
        if (savedMap == null)
        {
            // ��� ��带 Wall�� �ʱ�ȭ 
            InitializeMap();
            // ���� ��ġ�� ������� ������ ���� �ϳ��� ��쿡�� �� ���� 
            GenerateMazeUsingPrim();

            // ������ ���� ���¸� savedMap�� ����
            savedMap = nodes;
        }
        // ���� ������ �� ��ε� 
        else
        {
            LoadMap();
        }
    }


    // ��� ��带 Wall�� �ʱ�ȭ �Ѵ�. 
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
        // ������ ���� ���� ����
        // int startX = Random.Range(0, width);
        // int startY = Random.Range(0, height);

        // ���� ���� �߾ӿ� ��ġ 
        int startX = width / 2;
        int startY = height / 2;

        // ��带 None�� ���� 
        Node startNode = nodes[startX, startY];
        startNode.SetNodeType(NodeType.None);

        // ��� ��ó�� Wall�� ��� wallList�ȿ� ���� ���� 
        AddWallsToList(startNode);

        // wallList���� ������ ��� �ϳ��� ������ ������ ��尡 �ϳ��� ��쿡�� Wall ��带 None���� ��ȯ 
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

        // None ��� �߿��� Ȯ���� Moster ��峪 Event ���� �ٲ� 
        // �Ʒ� �ڵ�� �ٸ� ������ ������ ���� 
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
    // ����� �ֺ� �� ��带 WallList�� �ִ´�. 
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

    // ��� �ֺ��� None ��带 ��ȯ�Ѵ�. 
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