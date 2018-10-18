using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class House : Building {
    public static int CAPACITY = 5;

    [System.NonSerialized]
    private List<Human> habitants;


    private List<System.Guid> habitantsId;

    public List<System.Guid> HabitantsId {
        get { return habitantsId; }
        set { habitantsId = value; }
    }

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
        habitantsId = new List<System.Guid>();
        Guid = System.Guid.NewGuid();

    }

    public new House SimpleCopy() {
        House house = new House()
        {
            buildingType = buildingType,
            prefab = prefab,
            direction = direction,
            cost = cost,
            tag = tag,
            gameObject = GameObject,
            guid = System.Guid.NewGuid()


        };
        return house;
    }

    public static House FromBuilding(Building building) {
        House house = new House()
        {
            buildingType = BuildingType.HOUSE,
            prefab = building.Prefab,
            direction = building.Direction,
            cost = 100,
            tag = "house",
            gameObject = building.GameObject,
            guid = building.Guid
        };

        return house;
    }
}
