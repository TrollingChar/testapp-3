using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsLoader : MonoBehaviour {
    public GameObject
        worm;
    public Texture2D
        motherboard;

	void Awake () {
        Assets.worm = worm;
        Assets.motherboard = motherboard;
        Destroy(this);
	}
}
