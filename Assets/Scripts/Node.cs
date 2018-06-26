using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public int x;
    public int y;

    public int dCost;
    public int dHeuristic;

    public Node parent;


    public Node(int x, int y, int dCost, Node parent)
    {
        this.x = x;
        this.y = y;

        this.dCost = dCost;
        this.parent = parent;
    }

    public int dTotal
    {
        get { return dCost + dHeuristic; }
    }

    public bool equal(Node e)
    {
        return x == e.x && y == e.y;
    }

}
