using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private Game currentGame;
    
    private void instantiateObjects()
    {
        if (currentGame.object1.gameObject == null)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            currentGame.object1.gameObject = cube;
        }
        if (currentGame.object2.gameObject == null)
        {
            GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            currentGame.object2.gameObject = cube2;
        }
        currentGame.object1.setPosition(currentGame.object1.position);
        currentGame.object2.setPosition(currentGame.object2.position);
    }

    void Start()
    {
        currentGame = Game.getInstance();
        instantiateObjects();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Save"))
        {
            SaveLoad.Save();
        }
        if (GUILayout.Button("Load"))
        {
            Destroy(currentGame.object1.gameObject);
            Destroy(currentGame.object2.gameObject);

            SaveLoad.Load();
            currentGame = Game.getInstance();
            instantiateObjects();
        }

    }
}
