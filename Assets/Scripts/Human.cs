using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Human  {
    private Building target;
    private bool hasAHouse;

    public Building Target
    {
        get { return target; }
        set { target = value; }
    }

    public bool HasAHouse
    {
        get { return hasAHouse; }
        set { hasAHouse = value; }
    }

}
