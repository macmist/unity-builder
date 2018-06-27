using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;



public class MouseControl : MonoBehaviour
{
    public GameObject go;
    public float moveSpeed = 0.1f;
    private bool pathDone = false;
    private MapGenerator.TileObject[,] map;
    private List<GameObject> road;
    private Node start;
    private Node end;
    private AStar astar;
    private Vector3 mousePos;




    void Start()
    {
        go = Instantiate(go, Vector3.zero, Quaternion.identity);
        go.transform.position = new Vector3(0, 1, 0);
        astar = new AStar();
    }


    void Update()
    {
        if (Game.getInstance().enableMouse)
        {
            Vector3 newy = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                newy = hit.point;

            }
            newy.y = 1;

            Vector3 pos = new Vector3(Mathf.Round(newy.x), Mathf.Round(newy.y), Mathf.Round(newy.z));
            pos = StayInMap(pos);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                start = new Node((int)pos.x, (int)pos.z, 0, null);
                road = new List<GameObject>();
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (end == null || end.x != pos.x || end.y != pos.z)
                {
                    end = new Node((int)pos.x, (int)pos.z, 0, null);
                    map = Game.getInstance().map;
                    Node path = astar.GetPath(map, start, end);
                    PrintRoad(path);
                }
            }

           
            
        }
    }

    public Node PickRandomNode(int xBegin, int xEnd, MapGenerator.TileObject[,] map)
    {
        int x = UnityEngine.Random.Range(xBegin, xEnd);
        int y = UnityEngine.Random.Range(0, map.GetLength(1));

        while (map[x, y].Type == MapGenerator.Tile.TREE)
        {
            x = UnityEngine.Random.Range(xBegin, xEnd);
            y = UnityEngine.Random.Range(0, map.GetLength(1));
        }
        return new Node(x, y, 0, null);
    }

    public void PrintRoad(Node path)
    {
        foreach (GameObject g in road)
            Destroy(g);
        road.Clear();

        if (path == null) {
            return;
        }
        Node current = path;
        while(current != null)
        {
            Vector3 position = new Vector3(current.x, 1, current.y);
            GameObject r = Instantiate(go, position, Quaternion.identity);
            if (current.direction >= Direction.UP)
                r.transform.Rotate(0, 90, 0);
            road.Add(r);
            current = current.parent;
        }
    }


    private void DropObject()
    {
        go = Instantiate(go, Vector3.zero, Quaternion.identity);
    }

    /// <summary>
    /// Make sure the cube is stil in the map
    /// </summary>
    private Vector3 StayInMap(Vector3 newPos)
    {
        if (Game.getInstance().map != null)
        {
            MapGenerator.TileObject[,] map = Game.getInstance().map;
            int w = map.GetLength(0);
            int h = map.GetLength(1);
            if (newPos.x >= w)
                newPos.x = w - 1;
            if (newPos.x < 0)
                newPos.x = 0;
            if (newPos.z >= h)
                newPos.z = h - 1;
            if (newPos.z < 0)
                newPos.z = 0;
            go.transform.position = newPos;
        }
        mousePos = newPos;
        return newPos;
    }
}
