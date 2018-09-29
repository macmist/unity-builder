using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class House : Building {
    public static int CAPACITY = 5;
    private List<Human> habitants;

    public List<Human> Habitants
    {
        get { return habitants; }
        set { habitants = value; }
    }

    public House() {
        this.BuildingType = BuildingType.HOUSE;
        this.Direction = Direction.DOWN;
        this.cost = 100;
        this.tag = "house";
        habitants = new List<Human>();
    }
}
