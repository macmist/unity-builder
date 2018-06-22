using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



public class MouseControl : MonoBehaviour
{
    public GameObject go;
    public float moveSpeed = 0.1f;
    
    void Start()
    {
        go = GameObject.CreatePrimitive(PrimitiveType.Cube);
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
        }

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
