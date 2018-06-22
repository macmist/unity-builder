using System.Collections;
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

            SaveLoad.Load();
            currentGame = Game.getInstance();
            To3D(currentGame.map);
        }
        if (GUILayout.Button("Create Map"))
        {
            MapGenerator.TileObject[,] map = generator.CreateMap(centerNumbers);
            To3D(map);
        }
    }

    private void To3D(MapGenerator.TileObject[,] map) {
        if (objectMap != null)
        {
            for (int i = 0; i < map.GetLength(0); ++i)
            {
                for (int j = 0; j < map.GetLength(1); ++j)
                {
                    Destroy(objectMap[i, j]);
                }
            }
        }
        objectMap = new GameObject[map.GetLength(0), map.GetLength(1)];
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
            }
        }
        Game.getInstance().map = map;
        Game.getInstance().enableMouse = true;
    }
}
