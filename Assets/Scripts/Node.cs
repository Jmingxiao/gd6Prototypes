using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Node : IComparable<Node>
{
    public bool walkable;
    public int gridX;
    public int gridY;
    public int gcost;
    public int hcost;
    public Node parent;
    public Node cell;
    public Vector3 worldPos;
    public Node(bool walkable_, Vector3 worldPos_, int gridx, int gridy)
    {

        walkable = walkable_;
        gridX = gridx;
        gridY = gridy;
        worldPos = worldPos_;
    }
    public int fcost
    {
        get { return gcost + hcost; }
    }

    int IComparable<Node>.CompareTo(Node other)
    {
        if (other.fcost > this.fcost)
        {
            return -1;
        }else if (other.fcost < this.fcost)
        {
            return 1;
        }
        else
        {
            if (other.hcost > this.hcost)
            {
                return -1;
            }else if(other.hcost < this.hcost)
            {
                return 1;
            }
            return 0;
        }

    }
    
}