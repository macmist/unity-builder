using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanEntity : MonoBehaviour {
    private Human human;
    private float speed;

    public Human Human
    {
        get { return human; }
        set { human = value; }
    }
   

	// Use this for initialization
	void Start () {
        speed = Random.Range(5.0f, 10.0f);

    }
	
	// Update is called once per frame
	void Update () {
		if (human != null)
        {
            Vector3 targetPosition = human.Target.GameObject.transform.position;
            if (transform.position.Equals(targetPosition)) {
                if (!human.HasAHouse)
                {
                    human.HasAHouse = true;
                    Game.getInstance().people.AddHuman(human);
                    ((House)human.Target).Habitants.Add(human);
                }
            }
            else {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            }
        }
	}
}
