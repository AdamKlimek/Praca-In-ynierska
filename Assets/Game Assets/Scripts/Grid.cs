using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{

    Node[,] Graph;
   // public Transform enemy;
    public float NodeRadius;
    public Terrain terrain;
    float GridHeight;
    float GridWidth;
    int GridSizeX, GridSizeY;
    float NodeDiameter;
    public List<Node> path;

    void Start()
    {
        path = new List<Node>();
        NodeDiameter = NodeRadius * 2;
        GridHeight =terrain.terrainData.size.x;
        GridWidth = terrain.terrainData.size.z;
        GridSizeX = Mathf.RoundToInt(GridHeight / NodeDiameter);
        GridSizeY = Mathf.RoundToInt(GridWidth / NodeDiameter);

        CreateGrid();
    }

    void CreateGrid()
    {
        Graph = new Node[GridSizeX, GridSizeY];

        Vector3 WorldUpLeft = transform.position - Vector3.right * GridHeight / 2 + Vector3.forward * GridWidth / 2;

        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                Vector3 Worldpoint = WorldUpLeft + Vector3.right * (x * NodeDiameter + NodeRadius) - Vector3.forward * (y * NodeDiameter + NodeRadius);
                RaycastHit hit;
                bool walkable;
                Vector3 Dir = Vector3.down * 200;
                if (Physics.Raycast(Worldpoint, Dir, out hit, Mathf.Infinity, LayerMask.GetMask("Unwakable")))
                {
                   walkable = false;
                }
                else
                {
                    walkable = true;
                }
               // Worldpoint.y = hit.point.y;
                 Graph[x, y] = new Node(walkable, Worldpoint,x,y);
            }
        }

    }

    public Node NodeFromWorldPosition(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + GridHeight / 2) / GridHeight;
        float percentY = (worldPosition.z + GridWidth / 2) / GridWidth;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int X = Mathf.RoundToInt((GridSizeX - 1) * percentX);
        int Y = Mathf.RoundToInt((GridSizeY - 1) * percentY);
        Y = GridSizeY - Y-1;
        return Graph[X, Y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < GridSizeX && checkY >= 0 && checkY < GridSizeY)
                {
                    neighbours.Add(Graph[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    void OnDrawGizmos()
    {
        if (Graph != null)
        {
            foreach (Node n in Graph)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                if (path != null)
                    if (path.Contains(n))
                        Gizmos.color = Color.black;
                Gizmos.DrawCube(n.WorldPosition, Vector3.one * NodeDiameter);
            }
        }
    }

}