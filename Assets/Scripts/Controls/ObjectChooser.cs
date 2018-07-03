using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChooser : MonoBehaviour {
    private Building current;
    private MouseControl mouseController;
	// Use this for initialization
	void Start () {
        mouseController = GetComponent<MouseControl>();
	}

    private void LoadRoad() {
        if (current == null || current.BuildingType != BuildingType.ROAD)
        {
            GameObject road = Resources.Load("Prefabs/road", typeof(GameObject)) as GameObject;
            current = new Road();
            current.Prefab = road;
            mouseController.SetCurrentBuilding(current);
        }
    }

    private void LoadHouse()
    {
        if (current == null || current.BuildingType != BuildingType.HOUSE)
        {
            GameObject house = Resources.Load("Prefabs/house", typeof(GameObject)) as GameObject;
            current = new House();
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
