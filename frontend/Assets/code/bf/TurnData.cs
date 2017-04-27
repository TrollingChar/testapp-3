using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class TurnData {
    public bool w, a, s, d, mb;
    public Vector2 xy;

    public TurnData () {
        mb = Input.GetMouseButtonDown(0); // LMB
    }
}
