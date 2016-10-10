using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TerrainGrid : MonoBehaviour
{
    public Terrain terrain;
    public int NodeSize = 100;
    int GridWidth;
    int GridHeight;
    public int GridSizeX, GridSizeY;

    //public int[,] NodePathCost; //fcost,gcost,hcost
    //int HCost;
    //int GCost;
    
   // {
   //     get
   //     {
   //         return GCost + HCost;
   //     }
   // }

    Material [] Materials;
    public Material Walkable;
    public Material Unwalkable;
    public Material Hero;
    public Material Oponent;
    public Material path;
    public Vector3 [] vertex;
    Vector2[] UV;
    GameObject empty;
     public Transform Enemy;
    int OldNode;

    void Start()
    {
        
        GridHeight = Mathf.RoundToInt(terrain.terrainData.size.x);
        GridWidth = Mathf.RoundToInt(terrain.terrainData.size.z);
        GridSizeX = GridHeight / NodeSize;
        GridSizeY = GridWidth / NodeSize;
        Materials = new Material[2 * (GridSizeX * GridSizeY)];
        vertex = new Vector3[7*(GridSizeX * GridSizeY)];
        UV = new Vector2[7 * (GridSizeX * GridSizeY)];

        for (int x = 0; x < 2*(GridSizeX* GridSizeY); x++)
        {
            Materials[x] = Walkable;
        }

        CreateGrid();
        empty.transform.position += Vector3.up * 10;
        OldNode = -1;
    }
    
    void CreateGrid()
    {
        empty = new GameObject();
        empty.name = "Graph";
        empty.AddComponent<MeshFilter>();
        empty.AddComponent<MeshRenderer>();
        empty.AddComponent<MeshCollider>();


        empty.GetComponent<Renderer>().materials = Materials;

        Mesh mesh = empty.GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        Vector3 WorldUpLeft = transform.position - Vector3.right * GridHeight / 2 + Vector3.forward * GridWidth / 2;
       
        int count = 0;
        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                Vector3 UpLeftPoint = WorldUpLeft + x * (Vector3.right * NodeSize) - y * (Vector3.forward * NodeSize);
                Vector3 UpRightPoint = UpLeftPoint + Vector3.right * NodeSize;
                Vector3 BottomleftPoint = UpLeftPoint - Vector3.forward * NodeSize;
                Vector3 BottomRightPoint = UpLeftPoint + Vector3.right * NodeSize - Vector3.forward * NodeSize;
                Vector3 Middle = UpLeftPoint + Vector3.right * (NodeSize / 2) - Vector3.forward * (NodeSize / 2);

                vertex[count] = UpLeftPoint;
                vertex[count+1] = UpRightPoint;
                vertex[count+2] = BottomleftPoint;
                vertex[count+3] = BottomRightPoint;
                vertex[count+4] = UpRightPoint;
                vertex[count+5] = BottomleftPoint;
                vertex[count + 6] = Middle;

                UV[count] = new Vector2(vertex[count].x, vertex[count].z);
                UV[count + 1] = new Vector2(vertex[count+1].x, vertex[count+1].z);
                UV[count + 2] = new Vector2(vertex[count+2].x, vertex[count + 2].z);
                UV[count + 3] = new Vector2(vertex[count+3].x, vertex[count + 3].z);
                UV[count + 4] = new Vector2(vertex[count+4].x, vertex[count + 4].z);
                UV[count + 5] = new Vector2(vertex[count+5].x, vertex[count + 5].z);
                UV[count + 6] = new Vector2(vertex[count + 6].x, vertex[count + 6].z);

                count += 7;
            }
        }
        UpdateNodes();
        UpdateHeight();

        count = 0;
        mesh.vertices = vertex;
        mesh.uv = UV;
        mesh.subMeshCount = 2 *(GridSizeX * GridSizeY);
        
        int counter = 0;
        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {

                mesh.SetTriangles(new int[] { count, count + 1, count + 2 }, counter);
                counter++;
                mesh.SetTriangles(new int[] { count + 3, count + 4, count + 5 }, counter);
                counter++;

                count += 7;
               
            }
        }
    }

    void UpdateNodes()
    {
        int counter = 0;
        Material [] UpdatedMaterials = new Material[2 * (GridSizeX * GridSizeY)];
        UpdatedMaterials = empty.GetComponent<Renderer>().materials;
        for (int i = 6; i < vertex.Length; i+=7)
        {
            RaycastHit hit;
            Vector3 Dir = Vector3.down*200;
            if (Physics.Raycast(vertex[i], Dir, out hit, Mathf.Infinity, LayerMask.GetMask("Unwakable")))
            {
                UpdatedMaterials[counter] = Unwalkable;
                counter++;
                UpdatedMaterials[counter] = Unwalkable;
                counter++;         
            }
            else
            {
                counter+=2;
            }
        }

        empty.GetComponent<Renderer>().materials = UpdatedMaterials;
    }

    void UpdateHeight()
    {
        for (int i = 0; i < vertex.Length; i++)
        {
            RaycastHit hit;
            Vector3 Dir = Vector3.down * 200;
            if (Physics.Raycast(vertex[i], Dir, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
            {
                vertex[i].y = hit.point.y;
            }
        }
    }

    void UpdatePosition()
    {
        Material[] UpdatedMaterials = new Material[2 * (GridSizeX * GridSizeY)];
        UpdatedMaterials = empty.GetComponent<Renderer>().materials;

        if (OldNode != -1 )
        {
            UpdatedMaterials[OldNode] = Walkable;
            if (OldNode % 2 == 0)
            {
                UpdatedMaterials[OldNode + 1] = Walkable;
            }
            else
            {
                UpdatedMaterials[OldNode - 1] = Walkable;
            }
        }

        UpdatedMaterials[NodeFromWorldPosition(Enemy.position)] = Oponent;

        if (NodeFromWorldPosition(Enemy.position) % 2 == 0)
        {
            UpdatedMaterials[NodeFromWorldPosition(Enemy.position) + 1] = Oponent;
        }
        else
        {
            UpdatedMaterials[NodeFromWorldPosition(Enemy.position) - 1] = Oponent;
        }

        OldNode = NodeFromWorldPosition(Enemy.position);

        empty.GetComponent<Renderer>().materials = UpdatedMaterials;
    }

    public int NodeFromWorldPosition(Vector3 position)
    {
        float percentX = (position.x + GridHeight / 2) / GridHeight;
        float percentY = (position.z + GridWidth / 2) / GridWidth;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int X = Mathf.RoundToInt((GridSizeX - 1) * percentX);
        int Y = Mathf.RoundToInt((GridSizeY - 1) * percentY);

        return 2 * (X * GridSizeY + (GridSizeY - Y - 1));
    }

    public List<int> GetNeighbours(int node)
    {
        List<int> neighbours = new List<int>();
       // node /= 2;
        int X = node / GridSizeY;
        int Y = node % GridSizeY;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                X += (x);
                Y += (y);
                if (X >= 0 && X < GridSizeX && Y >= 0 && Y < GridSizeY)
                {
                    neighbours.Add((X * GridSizeY + Y));
                }
            }
        }

        return neighbours;
    }

    public void Makepath(List<int> Path)
    {
        Material[] UpdatedMaterials = new Material[2 * (GridSizeX * GridSizeY)];
        UpdatedMaterials = empty.GetComponent<Renderer>().materials;
        foreach(int node in Path)
        {
            UpdatedMaterials[node] = path;
            //if (node % 2 == 0)
            //{
            //    UpdatedMaterials[node + 1] = path;
            //}
            //else
            //{
            //    UpdatedMaterials[node - 1] = path;
            //}
        }

        empty.GetComponent<Renderer>().materials = UpdatedMaterials;
    }



    void Update()
    {
        UpdatePosition();
    }

}
