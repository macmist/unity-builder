﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Direction
{
    NONE,
    LEFT,
    RIGHT,
    BOTH,
    UP,
    DOWN
}

public class Node {
    public int x;
    public int y;

    public int dCost;
    public int dHeuristic;
    public int directionCost;
    public Direction direction;

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
        get { return dCost + dHeuristic + directionCost; }
    }

    public bool equal(Node e)
    {
        return x == e.x && y == e.y;
    }

}
