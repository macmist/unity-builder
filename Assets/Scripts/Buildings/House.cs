using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class House : Building {
    public House() {
        this.BuildingType = BuildingType.HOUSE;
        this.Direction = Direction.DOWN;
    }
}
