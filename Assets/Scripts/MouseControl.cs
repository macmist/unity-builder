using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;



public class MouseControl : MonoBehaviour
{
    private GameObject go;
    public float moveSpeed = 0.1f;

    private MapGenerator.TileObject[,] map;
    private List<GameObject> road;
    private Node start;
    private Node end;
    private AStar astar;
    private DropableObject currentBuilding;
    private Vector3 mousePos;
    private float offset;
    private float groundHeight = 1;

    public void SetCurrentBuilding(DropableObject building) {
        currentBuilding = building;
        if (Game.getInstance().enableMouse) {
            if (go != null)
                Destroy(go);
            go = Instantiate(currentBuilding.Prefab, Vector3.zero, Quaternion.identity);
            go.transform.position = new Vector3(0, 1, 0);
            currentBuilding.GameObject = go;
            offset = 1;
            Collider col = go.GetComponent<Collider>();
            if (col != null)
                offset = col.bounds.size.y / 2 + groundHeight / 2;
        }
    }

    void Start()
    {

        astar = new AStar();
    }


    void Update()
    {
        if (Game.getInstance().enableMouse && currentBuilding != null)
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
            mousePos = pos;
            if (currentBuilding.Prefab != null)
            {
                if (currentBuilding.Building == Building.ROAD)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        start = new Node((int)pos.x, (int)pos.z, 0, null);
                        road = new List<GameObject>();
                    }
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        if (end == null || end.x != (int)pos.x || end.y != (int)pos.z)
                        {
                            end = new Node((int)pos.x, (int)pos.z, 0, null);
                            map = Game.getInstance().map;
                            Node path = astar.GetPath(map, start, end);
                            PrintRoad(path);
                        }
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        DropObject();
                    }
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
        {
            Destroy(g);
            Vector3 position = g.transform.position;
            MapGenerator.TileObject tile = map[(int)position.x, (int)position.z];
            if (tile.Building != null && tile.Building.Building == Building.ROAD) {
                map[(int)position.x, (int)position.z].Building = null;
            }
        }
        road.Clear();

        if (path == null) {
            return;
        }
        Node current = path;
        while(current != null)
        {

            MapGenerator.TileObject tile = map[current.x, current.y];
            if (tile.Building == null)
            {
                Vector3 position = new Vector3(current.x, offset, current.y);
                GameObject r = Instantiate(currentBuilding.Prefab, position, Quaternion.identity);
                if (current.direction >= Direction.UP)
                    r.transform.Rotate(0, 90, 0);
                road.Add(r);
                DropableObject building = new DropableObject();
                building.GameObject = r;
                building.Prefab = currentBuilding.Prefab;
                building.Building = Building.ROAD;
                building.Direction = current.direction;
                map[current.x, current.y].Building = building;
            }
            current = current.parent;
        }
    }


    private void DropObject()
    {
        if (Game.getInstance().map != null && mousePos != null)
        {
            if (Game.getInstance().map[(int)mousePos.x, (int)mousePos.z].Building == null)
            {
                DropableObject dropable = new DropableObject();
                dropable.GameObject = Instantiate(currentBuilding.Prefab, mousePos, Quaternion.identity);;
                dropable.Prefab = currentBuilding.Prefab;
                dropable.Building = currentBuilding.Building;
                Game.getInstance().map[(int)mousePos.x, (int)mousePos.z].Building = dropable;
            }

        }
    }

    /// <summary>
    /// Make sure the cube is stil in the map
    /// </summary>
    private Vector3 StayInMap(Vector3 newPos)
    {
        if (map == null)
            map = Game.getInstance().map;
        if (map != null)
        {
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
            newPos.y = offset;
            go.transform.position = newPos;
        }
        return newPos;
    }
}
