using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[System.Serializable]
public class MapGenerator {
    public int w = 50;
    public int h = 50;
    public int centerPointNumber = 10;
    public TileObject[,] map;
    public List<TileObject> centerPoints;

    [System.Serializable]
    public class TileObject
    {
        private SerializableVector2 position;
        private bool centerPoint;
        private Tile type;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool CenterPoint
        {
            get { return centerPoint; }
            set { centerPoint = value; }
        }

        public Tile Type
        {
            get { return type; }
            set { type = value; }
        }

        public TileObject(int x, int y)
        {
            position = new Vector2(x, y);
        }
    }

    [System.Serializable]
    public enum Tile
    {
        GRASS,
        TREE,
        DIRT
    }

    /// <summary>
    /// Creates a new map and return it
    /// </summary>
    /// <returns></returns>
    public TileObject[,] CreateMap()
    {
        InitMap();
        PickCenterPoints();
        UpdateMap();
        return map;
    }


    /// <summary>
    /// Creates a new map with centerNumber centers and return it
    /// </summary>
    /// <param name="centerNumber">The new number of centers</param>
    /// <returns></returns>
    public TileObject[,] CreateMap(int centerNumber)
    {
        centerPointNumber = centerNumber;
        InitMap();
        PickCenterPoints();
        UpdateMap();
        return map;
    }

    /// <summary>
    /// Inits the map with default values
    /// </summary>
    private void InitMap()
    {
        centerPoints = new List<TileObject>();
        map = new TileObject[w, h];
        for (int i = 0; i < w; ++i)
        {
            for (int j = 0; j < h; ++j)
            {
                map[i, j] = new TileObject(i, j);
            }
        }
    }

    /// <summary>
    /// Randomly picks centerPointNumber of centerPoints
    /// </summary>
    private void PickCenterPoints()
    {
        // Reduce number of center points if there it's too important
        if (centerPointNumber >= w * h / 2)
            centerPointNumber /= 4;
        for (int i = 0; i < centerPointNumber; ++i)
        {
            int x = Random.Range(0, w);
            int y = Random.Range(0, h);

            Tile tile = Tile.GRASS;
            float random = Random.value;
            if (random > 0.8)
                tile = Tile.TREE;
            else if (random > 0.7)
                tile = Tile.DIRT;
            
            map[x, y].CenterPoint = true;
            map[x, y].Type = tile;
            centerPoints.Add(map[x, y]);
        }
    }

    /// <summary>
    /// Loop through all tiles, get the closest center, and give the tile the same type as the center
    /// </summary>
    private void UpdateMap()
    {
        for (int i = 0; i < w; ++i)
        {
            for (int j = 0; j < h; ++j)
            {
                TileObject closest = GetClosestCenter(map[i, j]);
                map[i, j].Type = closest.Type;
            }
        }
    }

    /// <summary>
    /// Loops through all centerpoints to return the closest from the object
    /// </summary>
    /// <param name="tile">The object to search</param>
    /// <returns></returns>
    private TileObject GetClosestCenter(TileObject tile)
    {
        TileObject closest = centerPoints[0];
        float distance = int.MaxValue;
        foreach(TileObject center in centerPoints)
        {
            float d = Distance2(center, tile);
            if (d < distance)
            {
                closest = center;
                distance = d;
            }
        }
        return closest;
    }

    /// <summary>
    /// Returns the square distance of 2 objects
    /// </summary>
    /// <param name="obj1">The first object</param>
    /// <param name="obj2">The second object</param>
    /// <returns></returns>
    private float Distance2(TileObject obj1, TileObject obj2)
    {
        float dx = obj1.Position.x - obj2.Position.x;
        float dy = obj1.Position.y - obj2.Position.y;

        return dx * dx + dy * dy;
    }
}
