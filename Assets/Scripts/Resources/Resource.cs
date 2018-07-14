using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource  {
    private string name;
    private string image;
    private int baseAmount;
    private int currentAmount;

    public string Name { get { return name; } set { name = value; }}
    public string Image { get { return Image; } set { image = value; }}
    public int BaseAmount { get { return baseAmount; } set { baseAmount = value; }}
    public int CurrentAmount { get { return currentAmount; } set { currentAmount = value; } }


    public Resource(string name, string image, int defaultValue) {
        this.name = name;
        this.image = image;
        this.baseAmount = defaultValue;
        this.currentAmount = defaultValue;
    }

    public void Add(int amount)
    {
        currentAmount += amount;
    }
}
