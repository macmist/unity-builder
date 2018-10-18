using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Human {
    [System.NonSerialized]
    private House target;

    [SerializeField]
    private System.Guid houseGuid;

    [SerializeField]
    private bool hasAHouse;

    [SerializeField]
    private readonly System.Guid guid;

    [System.NonSerialized]
    private HumanEntity entity;

    [SerializeField]
    private SerializableVector3 position;

    public House Target
    {
        get { return target; }
        set { 
            target = value;
            if (value != null)
                houseGuid = value.Guid;
        }
    }

    public bool HasAHouse
    {
        get { return hasAHouse; }
        set { hasAHouse = value; }
    }

    public void ReachedHouse() {
        if (!target.Habitants.Contains(this))
            target.Habitants.Add(this);
        if (!target.HabitantsId.Contains(this.guid))
            target.HabitantsId.Add(this.guid);
        hasAHouse = true;
    }

    public System.Guid Guid {
        get { return guid; }
    }

    public Human() {
        guid = System.Guid.NewGuid();
    }

    public HumanEntity Entity {
        get { return entity; }
        set { entity = value; }
    }

    public SerializableVector3 Position {
        get { return position; }
        set { position = value; }
    }

    public void Load() {
        if (hasAHouse && target != null) {
            target.Habitants.Add(this);
        }
    }

    public System.Guid HouseGuid {
        get { return houseGuid; }
        set { houseGuid = value; }
    }

}
