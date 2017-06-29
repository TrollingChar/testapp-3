using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace W3 {
    public class World {
        public float gravity;
        public float waterLevel;
        public Land land;
        public Tiles tiles;
        LinkedList<Object> objects;

        const float precision = 0.0001f;

        public World (Texture2D tex, SpriteRenderer renderer) {
            gravity = -0.5f;
            waterLevel = 0;
            tiles = new Tiles();

            land = new Land(
                new LandGen(new byte[,] {
                    {0, 0, 0, 0, 0},
                    {0, 1, 1, 1, 0},
                    {0, 1, 0, 1, 0}
                })
                .SwitchDimensions()
                .Expand(7)
                .Cellular(0x01e801d0, 20)
                .Cellular(0x01f001e0)
                .Expand()
                .Cellular(0x01e801d0, 20)
                .Cellular(0x01f001e0)
                .Rescale(2000, 1000)
                .Cellular(0x01f001e0),

                tex,
                renderer
            );
            objects = new LinkedList<Object>();
        }

        public void Update (TurnData td) {
            //Debug.Log(objects.Count);

            if (Core.bf.state.timer % 1000 == 0 && td != null && td.mb) {
                // spawn objects
            }

            /*if (td != null) {
                if (td.mb) {
                    Core.bf.worm.position = td.xy;
                    Core.bf.worm.velocity = XY.left;// 5 * new XY(RNG.Float() - RNG.Float(), RNG.Float() - RNG.Float());
                }
            }*/

            foreach (var o in objects) o.Update();

            foreach (var o in objects) {
                o.movement = 1;
                o.excluded.Clear();
                o.ExcludeObjects();
            }
            
            for (int i = 0, iter = 5; i < iter; i++) {
                foreach (var o in objects) {
                    if (o.velocity.length * o.movement <= precision) continue;

                    Collision c = o.NextCollision();
                    if (c == null) {
                        // no collision
                        o.position += o.movement * (1 - precision) * o.velocity;
                        o.movement = 0;

                    } else if (c.collider2 == null) {
                        // collided with land
                        o.position += c.offset.WithLengthReduced(precision);
                        o.movement -= Mathf.Sqrt(c.offset.sqrLength / o.velocity.sqrLength);
                        o.velocity = Geom.Bounce(
                            o.velocity,
                            c.normal,
                            Mathf.Sqrt(c.collider1.tangentialBounce * land.tangentialBounce),
                            Mathf.Sqrt(c.collider1.normalBounce * land.normalBounce)
                        );
                        o.OnCollision(c);

                    } else if (i > iter - 3) {
                        // force hard collision as if second object was land
                        // as long as magic number equals to 3, the eternal balance in the world will remain
                        o.position += c.offset.WithLengthReduced(precision);
                        c.collider2.obj.OnCollision(-c);
                        o.movement -= Mathf.Sqrt(c.offset.sqrLength / o.velocity.sqrLength);
                        o.velocity = Geom.Bounce(
                            o.velocity,
                            c.normal,
                            Mathf.Sqrt(c.collider1.tangentialBounce * c.collider2.tangentialBounce),
                            Mathf.Sqrt(c.collider1.normalBounce * c.collider2.normalBounce)
                        );

                    } else {
                        o.position += c.offset.WithLengthReduced(precision);
                        o.movement -= Mathf.Sqrt(c.offset.sqrLength / o.velocity.sqrLength);

                        // найти скалярное произведение нормали столкновения и скорости второго объекта
                        // так мы узнаем мешает ли он движению первого
                        float temp = XY.Dot(c.normal, c.collider2.obj.velocity);
                        if (temp >= 0 || c.collider2.obj.movement * c.collider2.obj.velocity.length <= precision) {
                            // collision
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
            
            // clear all NullObjects WITHOUT INVALIDATING THEIR NODES!
            for (LinkedListNode<Object> node = objects.First; node != null; ) {
                if (node.Value is NullObject) {
                    var next = node.Next;
                    objects.Remove(node);
                    node = next;
                } else node = node.Next;
            }

            //var list = new LinkedList<Object>(objects.Where<Object>(o => !(o is NullObject)));
            //objects.Clear();
            //objects = list;
        }

        public void AddObject (Object o, XY position) {
            o.node = objects.AddLast(o);
            o.position = position;
            o.OnAdd();
            o.UpdateSpritePosition();
        }
    }
}