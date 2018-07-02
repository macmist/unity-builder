using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Building
{
    ROAD,
    HOUSE
}

[System.Serializable]
public class DropableObject {
    private Building building;
    private Direction direction;

    [System.NonSerialized]
    private GameObject gameObject;

    [System.NonSerialized]
    private GameObject prefab;

    public Building Building {
        get { return building; }
        set { building = value; }
    }

    public Direction Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    public GameObject GameObject {
        get { return gameObject; }
        set { gameObject = value; }
    }

    public GameObject Prefab
    {
        get { return prefab; }
        set { prefab = value; }
    }
}
