using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private Game currentGame;
    private MapGenerator generator;
    public GameObject terrain;
    public int centerNumbers = 100;
    
    private void instantiateObjects()
    {
        if (currentGame.object1.gameObject == null)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            currentGame.object1.gameObject = cube;
        }
        if (currentGame.object2.gameObject == null)
        {
            GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            currentGame.object2.gameObject = cube2;
        }
        currentGame.object1.setPosition(currentGame.object1.position);
        currentGame.object2.setPosition(currentGame.object2.position);
    }

    void Start()
    {
        currentGame = Game.getInstance();
        instantiateObjects();
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
            Destroy(currentGame.object1.gameObject);
            Destroy(currentGame.object2.gameObject);

            SaveLoad.Load();
            currentGame = Game.getInstance();
            instantiateObjects();
        }
        if (GUILayout.Button("Create Map"))
        {
            MapGenerator.TileObject[,] map = generator.CreateMap(centerNumbers);
            CreateTexture(map);
        }
    }

    private void CreateTexture(MapGenerator.TileObject[,] map)
    {
        Texture2D texture = new Texture2D(map.GetLength(0), map.GetLength(1));
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color color;
                if (map[x, y].CenterPoint)
                {
                    color = Color.black;
                }
                else if (map[x, y].Type == MapGenerator.Tile.DIRT)
                {
                    color = Color.magenta;
                }
                else if (map[x, y].Type == MapGenerator.Tile.GRASS)
                {
                    color = Color.green;
                }
                else color = Color.blue;
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        terrain.GetComponent<Renderer>().material.mainTexture = texture;
    }
}
