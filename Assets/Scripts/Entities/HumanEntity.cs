using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanEntity : MonoBehaviour {
    private Human human;
    private float speed;
    private static List<HumanEntity> housedHumans;
    private static List<HumanEntity> humans;
    private static List<HumanEntity> roamingHumans;

    public static List<HumanEntity> HousedHumans
    {
        get { return housedHumans; }
        set { housedHumans = value; }
    }

    public static List<HumanEntity> Humans
    {
        get { return humans; }
        set { humans = value; }
    }

    public static List<HumanEntity> RoamingHumans
    {
        get { return roamingHumans; }
        set { roamingHumans = value; }
    }

    public Human Human
    {
        get { return human; }
        set { human = value; }
    }

    public static int HumanCount {
        get { return humans != null ? humans.Count : 0; }
    }

    public static int HousedHumanCount
    {
        get { return housedHumans != null ? housedHumans.Count : 0; }
    }

    public static int RoamingHumanCount
    {
        get { return roamingHumans != null ? roamingHumans.Count : 0; }
    }



    private static void InitLists() {
        if (housedHumans == null)
            housedHumans = new List<HumanEntity>();
        if (roamingHumans == null)
            roamingHumans = new List<HumanEntity>();
        if (humans == null)
            humans = new List<HumanEntity>();
    }

    // Use this for initialization
    void Start () {
        speed = Random.Range(5.0f, 10.0f);
    }

    private void Awake()
    {
        InitLists();
        humans.Add(this);
    }

    // Update is called once per frame
    void Update () {

        if (human != null && human.Target != null)
        {
            if (human.Target.GameObject != null)
            {
                Vector3 targetPosition = human.Target.GameObject.transform.position;
                if (transform.position.Equals(targetPosition))
                {
                    if (!human.HasAHouse)
                    {
                        human.ReachedHouse();
                        housedHumans.Add(this);
                        roamingHumans.Remove(this);
                    }
                    else {
                        if (!housedHumans.Contains(this)) {
                            roamingHumans.Remove(this);
                            housedHumans.Add(this);
                            human.ReachedHouse();
                        }
                    }
                }
                else
                {
                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
                    human.Position = transform.position;
                }
            }
        }
	}
}
