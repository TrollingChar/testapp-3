using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class World {
    float gravity;
    float waterLevel;
    Land land;
    LinkedList<W3Object> objects;
    Tiles tiles;

    Core core;

    public World (Core core) {
        this.core = core;

        gravity = -0.5f;
        waterLevel = -500;
        objects = new LinkedList<W3Object>();
        tiles = new Tiles();
        land = new Land(
            new LandGen(new byte[,] {{0, 0, 0, 0, 0},
                                     {0, 1, 1, 1, 0},
                                     {0, 1, 0, 1, 0}})
            .SwitchDimensions()
            .Expand(7)
            .Cellular(0x01e801d0, 20)
            .Cellular(0x01f001e0)
            .Expand()
            .Cellular(0x01e801d0, 20)
            .Cellular(0x01f001e0)
            .Rescale(2000, 1000)
            .Cellular(0x01f001e0),

            core
        );
    }

    public void Update (TurnData data) {
        // gather input
        if (data == null) data = new TurnData();
    }
}
