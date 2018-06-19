using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Test class for serialization
 */
 [System.Serializable]
public class TestObject  {
    public string name;

    public SerializableVector3 position;

    [System.NonSerialized]
    public GameObject gameObject;

    public TestObject()
    {
        name = "";
        position = Vector3.zero;
    }

    public void setPosition(Vector3 position)
    {
        this.position = position;
        if (gameObject != null)
            gameObject.transform.position = position;
    }
}
