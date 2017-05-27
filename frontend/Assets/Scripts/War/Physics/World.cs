using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace W3 {
    public class World {
        float gravity;
        float waterLevel;
        public Land land;
        Tiles tiles;
        LinkedList<Object> objects;

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
            objects = new LinkedList<Object>();

            AddObject(new Worm(XY.zero));
        }

        public void Update (TurnData td) {

            if (Core.I.bf.state.timer % 100 == 0 && td != null && td.mb) {
                // spawn objects
            }

            foreach (var o in objects) o.Update();

            foreach (var o in objects) {
                o.movement = 1;
                o.excluded = new HashSet<Object>();
                o.ExcludeObjects();
            }

            for (int i = 0, iter = 5; i < iter; i++) {
                foreach (var o in objects) {
                    if (o.velocity.length * o.movement <= 0.01f) continue;

                    var c = o.NextCollision();
                    if (c == null) {
                        o.position += o.movement * 0.99f * o.velocity;
                        o.movement = 0;
                    } else if (c.collider2 == null) {
                        o.position += c.offset.WithLengthReduced(0.01f);
                        o.movement -= Mathf.Sqrt(c.offset.sqrLength / o.velocity.sqrLength);
                        o.velocity = Geom.Bounce(o.velocity,
                                                 c.normal,
                                                 Mathf.Sqrt(c.collider1.tangentialBounce * land.tangentialBounce),
                                                 Mathf.Sqrt(c.collider1.normalBounce * land.normalBounce));
                    } else if (i > iter - 3) {
                        // as long as magic number equals to 3, the eternal balance in the world will remain
                        o.position += c.offset.WithLengthReduced(0.01f);
                        o.movement -= Mathf.Sqrt(c.offset.sqrLength / o.velocity.sqrLength);
                        o.velocity = Geom.Bounce(o.velocity,
                                                 c.normal,
                                                 Mathf.Sqrt(c.collider1.tangentialBounce * c.collider2.tangentialBounce),
                                                 Mathf.Sqrt(c.collider1.normalBounce * c.collider2.normalBounce));
                    } else {
                        o.position += c.offset.WithLengthReduced(0.01f);
                        o.movement -= Mathf.Sqrt(c.offset.sqrLength / o.velocity.sqrLength);

                        // найти скалярное произведение нормали столкновения и скорости второго объекта
                        // так мы узнаем мешает ли он движению первого
                        float temp = XY.Dot(c.normal, c.collider2.obj.velocity);
                        if (temp >= 0 || c.collider2.obj.movement * c.collider2.obj.velocity.length <= 0.01) {

                            XY velocity = (c.collider1.obj.mass * c.collider1.obj.velocity +
                                           c.collider2.obj.mass * c.collider2.obj.velocity) /
                                          (c.collider1.obj.mass + c.collider2.obj.mass);
                            XY v1 = o.velocity - velocity;
                            XY v2 = c.collider2.obj.velocity - velocity;
                            float tangBounce = Mathf.Sqrt(c.collider1.tangentialBounce * c.collider2.tangentialBounce);
                            float normBounce = Mathf.Sqrt(c.collider1.normalBounce * c.collider2.normalBounce);
                            o.velocity = velocity + Geom.Bounce(v1, c.normal, tangBounce, normBounce);
                            c.collider2.obj.velocity = velocity + Geom.Bounce(v2, c.normal, tangBounce, normBounce);
                        }
                    }
                    if (o.position.y < waterLevel) o.Remove();
                }
            }
            // handle stuck objects:
            foreach (var o in objects) {
                o.UpdateSpritePosition();
                o.velocity *= 1 - o.movement;
            }

            // clear all NullObjects
            var list = new LinkedList<Object>(objects.Where<Object>(o => !(o is NullObject)));
            objects.Clear();
            objects = list;
        }

        public void AddObject (Object o) {
            o.node = objects.AddLast(o);
            o.OnAdd();
        }
    }
}