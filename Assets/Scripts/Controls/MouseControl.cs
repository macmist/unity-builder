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
    private Building currentBuilding;
    private Vector3 mousePos;
    private float offset;
    private float groundHeight = 1;
    private bool canPlaceRoad = true;

    public void SetCurrentBuilding(Building building) {
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
            if (Input.GetKeyDown("a")) {
                currentBuilding.Rotate(false);
            }
            if (Input.GetKeyDown("e"))
            {
                currentBuilding.Rotate(true);
            }
            if (currentBuilding.Prefab != null)
            {
                MapGenerator.TileObject obj = Game.getInstance().map[(int)mousePos.x, (int)mousePos.z];
                if (obj.Building == null && obj.Type != MapGenerator.Tile.TREEGRASS
                    && Game.getInstance().gold != null && Game.getInstance().gold.CurrentAmount - currentBuilding.Cost >= 0)
                {
                    currentBuilding.ApplyDefaultColor();
                    if (currentBuilding.BuildingType == BuildingType.ROAD)
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
                        Debug.Log("non");
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            DropObject();
                        }
                    }
                }
                else
                    currentBuilding.ChangeColor(Color.red);
                if (obj.Building != null && obj.Building.BuildingType == BuildingType.ROAD && currentBuilding.BuildingType == BuildingType.ROAD)
                {
                    if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        Debug.Log("UP");
                        PlaceRoad();
                    }
                }
                
            }
        }
    }

    public Node PickRandomNode(int xBegin, int xEnd, MapGenerator.TileObject[,] map)
    {
        int x = UnityEngine.Random.Range(xBegin, xEnd);
        int y = UnityEngine.Random.Range(0, map.GetLength(1));

        while (map[x, y].Type == MapGenerator.Tile.TREEGRASS)
        {
            x = UnityEngine.Random.Range(xBegin, xEnd);
            y = UnityEngine.Random.Range(0, map.GetLength(1));
        }
        return new Node(x, y, 0, null);
    }

    private void ClearRoad()
    {
        foreach (GameObject g in road)
        {
            Destroy(g);
            Vector3 position = g.transform.position;
            MapGenerator.TileObject tile = map[(int)position.x, (int)position.z];
            if (tile.Building != null && tile.Building.BuildingType == BuildingType.ROAD)
            {
                map[(int)position.x, (int)position.z].Building = null;
            }
        }
        road.Clear();
    }

    private void ColorRoad(Color color)
    {
        foreach (GameObject g in road)
        {
            Vector3 position = g.transform.position;
            MapGenerator.TileObject tile = map[(int)position.x, (int)position.z];
            if (tile.Building != null && tile.Building.BuildingType == BuildingType.ROAD)
            {
                map[(int)position.x, (int)position.z].Building.ChangeColor(color);
            }
        }
    }

    public void PrintRoad(Node path)
    {
        ClearRoad();

        if (path == null) {
            return;
        }
        Node current = path;
        Node previous = null;
        while(current != null)
        {
            MapGenerator.TileObject tile = map[current.x, current.y];
            if (tile.Building == null)
            {
                Vector3 position = new Vector3(current.x, offset, current.y);
                GameObject r = Instantiate(currentBuilding.Prefab, position, Quaternion.identity);
                if (current.parent == null && previous != null)
                    current.direction = previous.direction;
                if (current.direction >= Direction.UP)
                    r.transform.Rotate(0, 90, 0);
                road.Add(r);
                Building building = currentBuilding.SimpleCopy();
                building.GameObject = r;
                building.Direction = current.direction;
                map[current.x, current.y].Building = building;
            }
            previous = current;
            current = current.parent;
        }
        Game currentGame = Game.getInstance();
        if (road.Count > 0 && road[0] != null)
        {
            if (currentGame == null || currentGame.gold == null ||
                currentGame.gold.CurrentAmount - (road.Count * currentBuilding.Cost) < 0)
            {
                canPlaceRoad = false;
                ColorRoad(Color.red);
            }
            else
                canPlaceRoad = true;
        }
    }

    public void PlaceRoad()
    {
        if (canPlaceRoad)
        {
            foreach (GameObject g in road)
            {
                Vector3 position = g.transform.position;
                MapGenerator.TileObject tile = map[(int)position.x, (int)position.z];
                if (tile.Building != null && tile.Building.BuildingType == BuildingType.ROAD)
                {
                    map[(int)position.x, (int)position.z].Building.Create();
                }
            }
        }
        else
            ClearRoad();
    }


    private void DropObject()
    {
        if (Game.getInstance().map != null && mousePos != null)
        {
            MapGenerator.TileObject obj = Game.getInstance().map[(int)mousePos.x, (int)mousePos.z];
            if (obj.Building == null && obj.Type != MapGenerator.Tile.TREEGRASS
                && Game.getInstance().gold!= null && Game.getInstance().gold.CurrentAmount - currentBuilding.Cost > 0)
            {
                currentBuilding.Create();
                Building dropable = currentBuilding.SimpleCopy();
                dropable.GameObject = Instantiate(currentBuilding.Prefab, mousePos, Quaternion.identity);
                dropable.RotateToDirection();
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
