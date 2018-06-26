using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



public class MouseControl : MonoBehaviour
{
    public GameObject go;
    public float moveSpeed = 0.1f;
    private bool pathDone = false;
    
    void Start()
    {
        go = Instantiate(go, Vector3.zero, Quaternion.identity);
        go.transform.position = new Vector3(0, 1, 0);
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
            
            StayInMap(new Vector3(Mathf.Round(newy.x), Mathf.Round(newy.y), Mathf.Round(newy.z)));
            if (Input.GetKeyDown(KeyCode.Mouse0))
                DropObject();

            if (!pathDone)
            {
                pathDone = true;
                Node start = new Node(10, 10, 0, null);
                Node end = new Node(25, 30, 0, null);
                AStar astar = new AStar();
                Node path = astar.GetPath(Game.getInstance().map, start, end);
                PrintRoad(path);
            }
        }
    }

    public void PrintRoad(Node path)
    {
        if (path == null) {
            return;
        }
        Node current = path;
        while(current != null)
        {
            Vector3 position = new Vector3(current.x, 1, current.y);
            Instantiate(go, position, Quaternion.identity);
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
    private void StayInMap(Vector3 newPos)
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
    }
}
