using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Road : Building {

    public Road() {
        this.BuildingType = BuildingType.ROAD;
        this.cost = 5;
    }
}
