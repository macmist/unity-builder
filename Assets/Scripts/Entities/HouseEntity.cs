using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public class HouseEntity : MonoBehaviour
{
    private House house;

    public House House 
    {
        get { return house; }
        set { house = value; }
    }
}
