using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Building
{
    ROAD
}

[System.Serializable]
public class DropableObject {
    private Building building;
    private Direction direction;

    [System.NonSerialized]
    private GameObject gameObject;

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
}
