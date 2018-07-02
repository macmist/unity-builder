using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChooser : MonoBehaviour {
    private DropableObject current;
    private MouseControl mouseController;
	// Use this for initialization
	void Start () {
        mouseController = GetComponent<MouseControl>();
	}

    private void LoadRoad() {
        if (current == null || current.Building != Building.ROAD)
        {
            GameObject road = Resources.Load("Prefabs/road", typeof(GameObject)) as GameObject;
            current = new DropableObject();
            current.Prefab = road;
            current.Building = Building.ROAD;
            mouseController.SetCurrentBuilding(current);
        }
    }

    private void LoadHouse()
    {
        if (current == null || current.Building != Building.HOUSE)
        {
            GameObject house = Resources.Load("Prefabs/house", typeof(GameObject)) as GameObject;
            current = new DropableObject();
            current.Building = Building.HOUSE;
            current.Prefab = house;
            mouseController.SetCurrentBuilding(current);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown) {
            switch(Input.inputString) {
                case "1":
                    LoadRoad();
                    break;
                case "2":
                    LoadHouse();
                    break;
            }
        }
	}
}
