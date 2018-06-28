﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private Game currentGame;
    private MapGenerator generator;
    public int centerNumbers = 100;
    private GameObject[,] objectMap;
    public GameObject grass;
    public GameObject tree;
    public GameObject dirt;
    public bool stopAstar;


    void Start()
    {
        currentGame = Game.getInstance();
        generator = new MapGenerator();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Save"))
        {
            SaveLoad.Save();
        }
        if (GUILayout.Button("Load"))
        {
            DeleteMapFromScreen(currentGame.map);
            SaveLoad.Load();
            currentGame = Game.getInstance();
            To3D(currentGame.map);
        }
        if (GUILayout.Button("Create Map"))
        {
            DeleteMapFromScreen(currentGame.map);
            MapGenerator.TileObject[,] map = generator.CreateMap(centerNumbers);
            To3D(map);
        }
    }

    private void DeleteMapFromScreen(MapGenerator.TileObject[,] map) {
        if (map != null && objectMap != null)
        {
            for (int i = 0; i < map.GetLength(0); ++i)
            {
                for (int j = 0; j < map.GetLength(1); ++j)
                {
                    if (map[i, j].Building != null)
                    {
                        Destroy(map[i, j].Building.GameObject);
                    }
                    Destroy(objectMap[i, j]);
                }
            }
        }
    }

    private void To3D(MapGenerator.TileObject[,] map) {

        objectMap = new GameObject[map.GetLength(0), map.GetLength(1)];
        GameObject road = Resources.Load("Prefabs/road", typeof(GameObject)) as GameObject;
        for (int i = 0; i < map.GetLength(0); ++i)
        {
            for (int j = 0; j < map.GetLength(1); ++j)
            {
                Vector3 pos = new Vector3(i, 0, j);
                switch (map[i, j].Type)
                {
                    case MapGenerator.Tile.DIRT:
                        objectMap[i, j] = Instantiate(dirt, pos, Quaternion.identity);
                        break;
                    case MapGenerator.Tile.GRASS:
                        objectMap[i, j] = Instantiate(grass, pos, Quaternion.identity);
                        break;
                    case MapGenerator.Tile.TREE:
                        objectMap[i, j] = Instantiate(tree, pos, Quaternion.identity);
                        break;
                    default: break;
                }
                DropableObject b = map[i, j].Building;
                if (b != null)
                {
                    switch (b.Building)
                    {
                    case Building.ROAD:
                            GameObject go = Instantiate(road, new Vector3(i, 1, j), Quaternion.identity);
                            if (b.Direction >= Direction.UP)
                                go.transform.Rotate(0, 90, 0);
                            b.GameObject = go;
                        break;
                    }
                }
            }
        }
        Game.getInstance().map = map;
        Game.getInstance().enableMouse = true;
    }

    private void Update()
    {
        Game.getInstance().stopAstar = stopAstar;
    }
}
