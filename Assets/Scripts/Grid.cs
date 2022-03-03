using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform player;
    public LayerMask unwalkablemask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public Node[,] grid;

    float nodeDiameter;
    int gridSize_x, gridSize_y;
    public PathFinding path;

    // Start is called before the first frame update
    void Start()
    {
        path = new PathFinding(this);
        nodeDiameter = nodeRadius * 2;
        gridSize_x = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSize_y = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }
    void CreateGrid()
    {
        grid = new Node[gridSize_x, gridSize_y];
        Vector3 worldBottomLeft = transform.position - new Vector3(gridWorldSize.x / 2, gridWorldSize.y / 2, 0);

        for (int x = 0; x < gridSize_x; x++)
        {
            for (int y = 0; y < gridSize_y; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius)
                     + Vector3.up * (y * nodeDiameter + nodeRadius);

                 bool walkable = !(Physics2D.CircleCast(worldPoint, nodeRadius,Vector2.down,0.1f, unwalkablemask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }

    }

    public List<Node> GetNeighbours(Node current)
    {
        List<Node> neighbours = new List<Node>();
        Node parent = current.parent;
         for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (Mathf.Abs(x) == 1 && Mathf.Abs(y) == 1)
                        continue;
                    if (x == 0 && y == 0 )
                        continue;

                    int check_x = current.gridX + x;
                    int check_y = current.gridY + y;

                    if (check_x >= 0 && check_x < gridSize_x && check_y >= 0 && check_y < gridSize_y)
                    {
                        neighbours.Add(grid[check_x, check_y]);
                    }
                }
            }
        return neighbours;
    }
    public Node NodeFromWorldPoint(Vector2 worldPos)
    {
        float percent_x = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percent_y = (worldPos.y + gridWorldSize.y / 2) / gridWorldSize.y;

        percent_x = Mathf.Clamp01(percent_x);
        percent_y = Mathf.Clamp01(percent_y);

        int x = Mathf.RoundToInt((gridSize_x - 1) * percent_x);
        int y = Mathf.RoundToInt((gridSize_y - 1) * percent_y);

        return grid[x, y];
    }
    private void OnDrawGizmos() {
       if(grid!= null)
        {
            Node playerNode = NodeFromWorldPoint(player.position);

            foreach(Node node in grid)
            {
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
                if(playerNode == node)
                {
                    Gizmos.color = Color.cyan;
                }
                if (path != null)
                {
                    if (path.m_path.Contains(node))
                    {
                        Gizmos.color = Color.black;
                    }
                }

                Gizmos.DrawCube(node.worldPos, new Vector3(1,1,1)*(int)nodeDiameter);
            }
        }
    }
}
