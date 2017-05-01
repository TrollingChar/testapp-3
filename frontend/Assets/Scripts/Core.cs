using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
    public WSConnection connection;
    public World world;

    void Start () { }
	
	void Update () {
        connection.Work();
        if (world != null) world.Work(null);
	}
}
