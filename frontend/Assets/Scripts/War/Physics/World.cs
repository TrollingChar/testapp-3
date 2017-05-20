using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class World {
    float gravity;
    float waterLevel;
    Land land;
    Tiles tiles;
    LinkedList<W3Object> objects;

    public World () {
        gravity = -0.5f;
        waterLevel = -500;
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
            .Cellular(0x01f001e0)
        );
        objects = new LinkedList<W3Object>();
    }

    public void Update (TurnData td) {
        if (Core.I.bf.state.timer % 100 == 0 && td != null && td.mb) {
            // spawn objects
        }

        foreach (var o in objects) o.Update();

        foreach (var o in objects) {
            o.movement = 1;
            o.excluded = new HashSet<W3Object>();
            o.ExcludeObjects();
        }

        for (int i = 0, iter = 5; i < iter; i++) {
            foreach (var o in objects) {
                if (o.velocity.magnitude * o.movement <= 0.01f) continue;

                var c = o.NextCollision();
                if (c == null) {

                } else if (c.collider2 == null) {

                } else if (i > iter - 3) {
                    // as long as magic number equals to 3, the eternal balance in the world will remain
                } else {

                }
                if (o.position.y < waterLevel) o.Remove();
            }
        }
        foreach (var o in objects) {
            // todo update gfx

            // handle stuck objects
            o.velocity *= 1 - o.movement;
        }

        // clear all EmptyObjects
        var list = new LinkedList<W3Object>(objects.Where<W3Object>(o => !(o is EmptyObject)));
        objects.Clear();
        objects = list;
    }

    public void AddObject (W3Object o) {
        o.node = objects.AddLast(o);
    }
}
