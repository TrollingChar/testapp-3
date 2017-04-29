using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.code.bf;

class BF {
    float gravity;
    LandGen land;

    void Start (GameData data) {
        RNG.Init(data.seed);
        //GenMap();
    }

    void Update (TurnData data) {
    }
}
