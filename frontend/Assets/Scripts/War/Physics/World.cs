using System.Collections.Generic;
using Geometry;
using UnityEngine;
using War.Objects;
using Object = War.Objects.Object;


namespace War.Physics {

    public class World {

        public float gravity;
        public float waterLevel;
        public Land land;
        public Tiles tiles;
        private LinkedList<Object> objects;

        private const float precision = 0.1f;


        public World (Texture2D tex, SpriteRenderer renderer) {
            gravity = -0.5f;
            waterLevel = 0;
            tiles = new Tiles();

            land = new Land(
                new LandGen(
                        new byte[,] {
                            {0, 0, 0, 0, 0},
                            {0, 1, 1, 1, 0},
                            {0, 1, 0, 1, 0}
                        }
                    )
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

            if (Core.bf.state.timer % 500 == 0 && td != null && td.mb) {
                AddObject(new Worm(), td.xy);
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

                    var c = o.NextCollision();
                    Object o2 = c == null || c.collider2 == null ? null : c.collider2.obj;

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
                    } else if (i > iter - 3 || o.superMass < o2.superMass) {
                        // force hard collision as if second object was land
                        // as long as magic number equals to 3, the eternal balance in the world will remain
                        o.position += c.offset.WithLengthReduced(precision);
                        o.movement -= Mathf.Sqrt(c.offset.sqrLength / o.velocity.sqrLength);
                        o.velocity = Geom.Bounce(
                            o.velocity,
                            c.normal,
                            Mathf.Sqrt(c.collider1.tangentialBounce * c.collider2.tangentialBounce),
                            Mathf.Sqrt(c.collider1.normalBounce * c.collider2.normalBounce)
                        );
                        o.OnCollision(c);
                        o2.OnCollision(-c);
                    } else if (o2.superMass < o.superMass) {
                        // treat first object as it has infinite mass
                        o.position += c.offset.WithLengthReduced(precision);
                        o.movement -= Mathf.Sqrt(c.offset.sqrLength / o.velocity.sqrLength);
                        o.velocity = Geom.Bounce(
                            o.velocity,
                            c.normal,
                            Mathf.Sqrt(c.collider1.tangentialBounce * c.collider2.tangentialBounce),
                            Mathf.Sqrt(c.collider1.normalBounce * c.collider2.normalBounce)
                        );
                        o.OnCollision(c);
                        o2.OnCollision(-c);
                    } else {
                        o.position += c.offset.WithLengthReduced(precision);
                        o.movement -= Mathf.Sqrt(c.offset.sqrLength / o.velocity.sqrLength);

                        // найти скалярное произведение нормали столкновения и скорости второго объекта
                        // так мы узнаем мешает ли он движению первого
                        float temp = XY.Dot(c.normal, c.collider2.obj.velocity);
                        if (temp >= 0 || c.collider2.obj.movement * c.collider2.obj.velocity.length <= precision) {
                            // collision
                            XY velocity = (o.mass * o.velocity + o2.mass * o2.velocity) / (o.mass + o2.mass);
                            XY v1 = o.velocity - velocity;
                            XY v2 = o2.velocity - velocity;
                            float tangBounce = Mathf.Sqrt(c.collider1.tangentialBounce * c.collider2.tangentialBounce);
                            float normBounce = Mathf.Sqrt(c.collider1.normalBounce * c.collider2.normalBounce);
                            o.velocity = velocity + Geom.Bounce(v1, c.normal, tangBounce, normBounce);
                            o2.velocity = velocity + Geom.Bounce(v2, c.normal, tangBounce, normBounce);
                        }

                        o.OnCollision(c);
                        o2.OnCollision(-c);
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
            for (var node = objects.First; node != null;) {
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


        public void AddObject (Object o, XY position, XY velocity = default(XY)) {
            o.node = objects.AddLast(o);
            o.position = position;
            o.velocity = velocity;
            o.OnAdd();
            o.UpdateSpritePosition();
        }

    }

}
