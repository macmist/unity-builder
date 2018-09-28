using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tree : Building {

    public Tree()
    {
        this.BuildingType = BuildingType.TREE;
        this.cost = 0;
    }
}
