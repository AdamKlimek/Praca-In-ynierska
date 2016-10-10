using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GreedyBestFirstSearch : MonoBehaviour
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
                if (OpenSet[i].hCost < Current.hCost)
                {
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
                else
                {
                    neighbour.hCost = GetDistance(neighbour, TargetNode);
                    neighbour.parent = Current;

                    if (!OpenSet.Contains(neighbour))
                        OpenSet.Add(neighbour);
                }
                //int newCostToNeighbour = Current.gCost + GetDistance(Current, neighbour);
                //if ( !OpenSet.Contains(neighbour))
                //{
                //    //neighbour.gCost = newCostToNeighbour;
                //    neighbour.hCost = GetDistance(neighbour, TargetNode);
                //    neighbour.parent = Current;

                //    if (!OpenSet.Contains(neighbour))
                //        OpenSet.Add(neighbour);
                //}
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

    void MoveEnemy(List<Node> path)
    {
        ////Debug.Log(transform.position);
        //if (Vector3.Distance(TargetPoint.position, gameObject.transform.position) < 15)
        //{
        //    Debug.Log("przeciwnik dotarl do gracza");
        //}
        //else
        //{
        //    List<Node> MovePath = path;
        //    if (MovePath.Count > 0)
        //    {
        //        Vector3 dir = MovePath[MovePath.Count - 1].WorldPosition - gameObject.transform.position;
        //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3.0f);
        //        //RaycastHit hit;
        //        //Vector3 Dir = Vector3.down * 200;
        //        //if (Physics.Raycast(transform.FindChild("pivot").transform.position, Dir, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
        //        //{
        //        //    gameObject.transform.position.y = hit.point.y;
        //        //}
        //        gameObject.transform.Translate(transform.forward * 5.0f * Time.deltaTime, Space.World);
        //    }
        //}
    }
}
