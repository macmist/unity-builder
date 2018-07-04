using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum BuildingType
{
    ROAD,
    HOUSE
}

[System.Serializable]
public class Building {
    private BuildingType buildingType;
    private Direction direction;
    private bool stopped = false;
    private bool isDefaultColors = false;

    [System.NonSerialized]
    private Dictionary<string, Color> defaultColors;

    [System.NonSerialized]
    private GameObject gameObject;

    [System.NonSerialized]
    private GameObject prefab;

    public BuildingType BuildingType {
        get { return buildingType; }
        set { buildingType = value; }
    }

    public Direction Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    public GameObject GameObject {
        get { return gameObject; }
        set {
            gameObject = value;
            RegisterColors();
        }
    }

    public GameObject Prefab
    {
        get { return prefab; }
        set { prefab = value; }
    }

    /// <summary>
    /// Copy this instance prefab and type.
    /// </summary>
    /// <returns>The copy.</returns>
    public Building SimpleCopy() {
        Building building = new Building();
        building.buildingType = buildingType;
        building.prefab = prefab;
        building.direction = direction;
        return building;
    }

    /// <summary>
    /// Start this instance.
    /// </summary>
    public void Start() {
        stopped = false;
    }

    /// <summary>
    /// Stop this instance.
    /// </summary>
    public void Stop() {
        stopped = true;
    }

    public void Rotate(bool clockwise) {
        if (gameObject != null) {
            int rotation = 90 * (clockwise ? 1 : -1);
            gameObject.transform.Rotate(new Vector3(0, rotation, 0));
            switch(direction) {
                case Direction.DOWN:
                    direction = clockwise ? Direction.LEFT : Direction.RIGHT;
                    break;
                case Direction.UP:
                    direction = !clockwise ? Direction.LEFT : Direction.RIGHT;
                    break;
                case Direction.LEFT:
                    direction = clockwise ? Direction.UP : Direction.DOWN;
                    break;
                case Direction.RIGHT:
                    direction = !clockwise ? Direction.UP : Direction.DOWN;
                    break;
                default:break;
            }
        }
    }

    public void RotateToDirection() {
        if (gameObject != null)
        {
            int dir = 0;
            switch (this.direction)
            {
                case Direction.DOWN:
                    dir = 0;
                    break;
                case Direction.UP:
                    dir = 180;
                    break;
                case Direction.LEFT:
                    dir = 90;
                    break;
                case Direction.RIGHT:
                    dir = -90;
                    break;
                default: break;
            }
            gameObject.transform.Rotate(new Vector3(0, dir, 0));
        }
    }

    public void RotateToDirection(Direction newDirection)
    {
        direction = newDirection;
        RotateToDirection();
    }

    public void RegisterColors()
    {
        defaultColors = new Dictionary<string, Color>();
        Renderer rend = gameObject.GetComponent<Renderer>();
        foreach (Material mat in rend.materials)
        {
            defaultColors.Add(mat.name, mat.GetColor("_Color"));
        }
        isDefaultColors = true;
    }

    public void ChangeColor(Color color)
    {
        if (gameObject != null)
        {
            //Fetch the Renderer from the GameObject
            Renderer rend = gameObject.GetComponent<Renderer>();
            foreach (Material mat in rend.materials)
            {
                mat.SetColor("_Color", color);
            }
            isDefaultColors = false;
        }
    
    }

    public void ApplyDefaultColor()
    {
        if (gameObject != null && defaultColors != null && !isDefaultColors)
        {
            //Fetch the Renderer from the GameObject
            Renderer rend = gameObject.GetComponent<Renderer>();
            foreach (Material mat in rend.materials)
            {
                if (defaultColors.ContainsKey(mat.name))
                {
                    Color color = defaultColors[mat.name];
                    if (color != null)
                        mat.SetColor("_Color", color);
                }
            }
            isDefaultColors = true;
        }
    }
}
