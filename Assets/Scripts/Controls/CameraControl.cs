using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    public int speed = 15;
    private int boundary = 50;
    private int offset = 15;
    private int sw;
    private int sh;
    void Start()
    {
        sw = Screen.width;
        sh = Screen.height;
    }

    void Update()
    {
        if (Game.getInstance().enableMouse && Game.getInstance().map != null)
        {
            MapGenerator.TileObject[,] map = Game.getInstance().map;
            Vector3 pos = transform.position;
            int mw = map.GetLength(0);
            int mh = map.GetLength(1);
            if (Input.mousePosition.x > sw - boundary && transform.position.x < mw )
            {
                pos.x += speed * Time.deltaTime; // move on +X axis
            }
            if (Input.mousePosition.x < 0 + boundary && transform.position.x > 0 )
            {
                pos.x -= speed * Time.deltaTime; // move on -X axis
            }
            if (Input.mousePosition.y > sh - boundary && transform.position.z < mh - offset )
            {
                pos.z += speed * Time.deltaTime; // move on +Z axis
            }
            if (Input.mousePosition.y < 0 + boundary && transform.position.z > 0 - offset)
            {
                pos.z -= speed * Time.deltaTime; // move on -Z axis
            }


            transform.position = pos;
        }
    }
}
