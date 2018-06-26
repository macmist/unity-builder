using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar {
    List<Node> openList;
    private MapGenerator.TileObject[,] map;
    private Node end;

    private Node GetMostAccuratePoint()
    {
        Node n = openList[0];
        foreach(Node other in openList)
        {
            if (other.dTotal < n.dTotal)
                n = other;
        }
        openList.Remove(n);
        return n;
    }


    private int Distance2(Node point)
    {
        int dx = point.x - end.x;
        int dy = point.y - end.y;

        return dx * dx + dy * dy;
    }


    private List<Node> GetNeighbors(Node point)
    {
        List<Node> neighbors = new List<Node>();
        if (point.x - 1 > 0)
        {
            Node n = new Node(point.x - 1, point.y, point.dCost + 1, point);
            n.dHeuristic = Distance2(n);
            neighbors.Add(n);
        }
        if (point.x + 1 < map.GetLength(0))
        {
            Node n = new Node(point.x + 1, point.y, point.dCost + 1, point);
            n.dHeuristic = Distance2(n);
            neighbors.Add(n);
        }
        if (point.y - 1 > 0)
        {
            Node n = new Node(point.x, point.y -1, point.dCost + 1, point);
            n.dHeuristic = Distance2(n);
            neighbors.Add(n);
        }
        if (point.y + 1 < map.GetLength(0))
        {
            Node n = new Node(point.x, point.y + 1, point.dCost + 1, point);
            n.dHeuristic = Distance2(n);
            neighbors.Add(n);
        }
        return neighbors;
    }

    public bool HasWithLesserCost(List<Node> nodes, Node n)
    {
        foreach (Node e in nodes)
        {
            if (e.equal(n))
            {
                return e.dTotal < n.dTotal;
            }
        }
        return false;
    }
    
    public Node GetPath(MapGenerator.TileObject[,] map, Node start, Node end)
    {
        this.map = map;
        this.end = end;
        openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        openList.Add(start);
        if (start.equal(end))
        {
            return end;
        }
        while (openList.Count > 0)
        {
            Node a = GetMostAccuratePoint();
            if (end.equal(a))
            {
                return a;
            }
            foreach(Node neighbor in GetNeighbors(a))
            {
                if (HasWithLesserCost(openList, neighbor) || HasWithLesserCost(closedList, neighbor))
                    continue;
                openList.Add(neighbor);
            }
            closedList.Add(a);
        }
        return null;
    }
}
