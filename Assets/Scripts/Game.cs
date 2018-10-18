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
    public People people;

    [System.NonSerialized]
    private List<Human> housedHumans;

    [SerializeField]
    private List<Human> humans;

    [System.NonSerialized]
    private List<Human> roamingHumans;

    public  List<Human> HousedHumans
    {
        get { return housedHumans; }
        set { housedHumans = value; }
    }

    public List<Human> Humans
    {
        get { return humans; }
        set { humans = value; }
    }

    public List<Human> RoamingHumans
    {
        get { return roamingHumans; }
        set { roamingHumans = value; }
    }


    /// <summary>
    /// Private constructor to ensure it's only called when we want it
    /// </summary>
    private Game()
    {
        gold = new Resource("ARJEN", "", 1000);
        people = new People("people", "", 0);
    }

    public void BeforeSave()
    {
        humans = new List<Human>();
        if (HumanEntity.Humans != null)
        {
            HumanEntity.Humans.ForEach(delegate (HumanEntity h)
            {
                humans.Add(h.Human);
            });
        }

    }

    public void AfterSaveAndLoad(bool loaded)
    {

        //humans = null;
        //roamingHumans = null;
        //housedHumans = null;
        if (humans != null) {
            housedHumans = new List<Human>();
            roamingHumans = new List<Human>();
            foreach(Human h in humans) {
                if (h.HasAHouse)
                    housedHumans.Add(h);
                else
                    roamingHumans.Add(h);
            }
        }
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
