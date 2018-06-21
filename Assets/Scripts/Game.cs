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

    private Game()
    {
    }

    
    public static void setGame(Game game)
    {
        instance = game;
    }

    public static Game getInstance() {
        if (instance == null)
            instance = new Game();
        return instance;
    }
}
