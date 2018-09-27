using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/**
 * Represents the game. It's a singleton. 
 */
[System.Serializable]
public class Game {
    private static Game instance;
    public MapGenerator.TileObject[,] map;
    public bool enableMouse = false;
    public bool stopAstar = false;

    public Resource gold;
    public Resource people;

    /// <summary>
    /// Private constructor to ensure it's only called when we want it
    /// </summary>
    private Game()
    {
        gold = new Resource("ARJEN", "", 1000);
        people = new Resource("people", "", 0);
    }

    
    public static void setGame(Game game)
    {
        instance = game;
    }

    /// <summary>
    /// Returns the instance, creates it first if it does not exist
    /// </summary>
    /// <returns></returns>
    public static Game getInstance() {
        if (instance == null)
            instance = new Game();
        return instance;
    }
}
