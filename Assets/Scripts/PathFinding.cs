using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PathFinding 
{
    public Transform seeker,target;
    Node tarNode;
    Grid m_grid;
    public List<Node> m_path;
    public HashSet<Node> findedNode;
    public PathFinding(Grid grid_)
    {
        m_grid = grid_;
        m_path = new List<Node>();
        findedNode = new HashSet<Node>();
    }

    public void SetUp(){
        
        m_path = new List<Node>();
        findedNode = new HashSet<Node>();
        foreach (var node in m_grid.grid)
        {
            node.parent = null;
            node.cell = null;
        }
    }
    private void Update()
    {
        FindPath(seeker.position, target.position);
    }
   public void FindPath(Vector3 startPos ,Vector3 TargetPos)
    {
        SetUp();
        Node startNode = m_grid.NodeFromWorldPoint(startPos);
        tarNode = m_grid.NodeFromWorldPoint(TargetPos);
        List<Node> openSet = new List<Node>();
        HashSet<Node> closeSet = new HashSet<Node>();
        
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.Min();

            openSet.Remove(currentNode);
            closeSet.Add(currentNode);

            if (currentNode == tarNode)
            {
                RetracePath(startNode, tarNode);
                return;
            }
            foreach (Node neighbour in m_grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closeSet.Contains(neighbour))
                {
                    continue;
                }
                int MoveCostToNeighbour = currentNode.gcost + Getdist(currentNode, neighbour);
                if (MoveCostToNeighbour < neighbour.gcost || !openSet.Contains(neighbour))
                {
                    neighbour.gcost = MoveCostToNeighbour;
                    neighbour.hcost = Getdist(neighbour, tarNode);
                    neighbour.parent = currentNode;
                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                        findedNode.Add(neighbour);
                    }
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
       m_path = new List<Node>();

        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            m_path.Add(currentNode);
            findedNode.Remove(currentNode);
            Node cellnode= currentNode;
            currentNode = currentNode.parent;
            currentNode.cell = cellnode;
        }
        m_path.Reverse();
    }

    int Getdist(Node A , Node B)
    {
       int distX = Mathf.Abs(A.gridX - B.gridX);
        int distY = Mathf.Abs(A.gridY - B.gridY);
        return 10 * (distX + distY);
        
    }
}
