using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject nodePrefab;

    private static Node[,] savedMap = null;  // 저장된 맵 상태

    private void Start()
    {
        if (savedMap == null)
        {
            GenerateMap();
        }
        else
        {
            LoadMap();
        }
    }

    private void GenerateMap()
    {
        savedMap = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                NodeType type;
                float roll = Random.Range(0f, 1f);

                if (roll < 0.2f)  // 20%의 확률로 비어 있는 방 생성
                {
                    type = NodeType.Empty;
                }
                else
                {
                    type = (NodeType)Random.Range(0, System.Enum.GetValues(typeof(NodeType)).Length - 1); // -1 to exclude Empty from random selection
                }

                GameObject nodeObj = Instantiate(nodePrefab, new Vector3(x,y, 0), Quaternion.identity);
                Node nodeComponent = nodeObj.GetComponent<Node>();
                nodeComponent.SetNodeType(type);

                savedMap[x, y] = nodeComponent;
            }
        }
    }

    private void LoadMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject nodeObj = Instantiate(nodePrefab, new Vector3(x, y, 0), Quaternion.identity);
                Node nodeComponent = nodeObj.GetComponent<Node>();
                nodeComponent.SetNodeType(savedMap[x, y].Type);
            }
        }
    }
}
