using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class People : Resource
{
    private List<Human> humen;
    public List<Human> Humen
    {
        get { return humen; }
        set
        {
            humen = value;
            currentAmount = humen != null ? humen.Count : 0;
        }
    }

    public People(string name, string image, int defaultValue) : base(name, image, defaultValue)
    {
        humen = new List<Human>();
        currentAmount = 0;
    }

    public void AddHuman(Human human)
    {
        humen.Add(human);
        currentAmount = humen.Count;
    }
}