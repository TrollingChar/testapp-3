using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsLoader : MonoBehaviour {
    public GameObject
        // asset1,
        // asset2,
        worm;
    public Texture2D
        motherboard;

	void Awake () {
        Assets.worm = worm;
        Assets.motherboard = motherboard;
        Destroy(this);
	}
}
