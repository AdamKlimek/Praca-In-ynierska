using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarPathfinding : MonoBehaviour
{
    private Transform TargetPoint;
    Grid grid;

    void Start()
    {
        TargetPoint = GameObject.Find("Hero").transform;
        grid = GameObject.Find("Grid").GetComponent<Grid>();
    }

    void Update()
    {
        FindPath();
    }

    void FindPath()
    {
        Node TargetNode = grid.NodeFromWorldPosition(TargetPoint.position);
        Node StartNode = grid.NodeFromWorldPosition(gameObject.transform.position);

        List<Node> OpenSet = new List<Node>();
        HashSet<Node> ClosedSet = new HashSet<Node>();

        OpenSet.Add(StartNode);

        while (OpenSet.Count > 0)
        {
            Node Current = OpenSet[0];
            for (int i = 1; i < OpenSet.Count; i++)
            {
                if (OpenSet[i].fCost < Current.fCost || OpenSet[i].fCost == Current.fCost)
                {
                    if (OpenSet[i].hCost < Current.hCost)
                        Current = OpenSet[i];
                }
            }

            OpenSet.Remove(Current);
            ClosedSet.Add(Current);

            if (Current == TargetNode)
            {
                 RetracePath(StartNode, TargetNode);

                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(Current))
            {
                if (!neighbour.walkable || ClosedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = Current.gCost + GetDistance(Current, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !OpenSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, TargetNode);
                    neighbour.parent = Current;

                    if (!OpenSet.Contains(neighbour))
                        OpenSet.Add(neighbour);
                }
            }
        }
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        else
        {
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;

        gameObject.SendMessage("Move");
    }
}