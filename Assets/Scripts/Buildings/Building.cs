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
        set { gameObject = value; }
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
}
