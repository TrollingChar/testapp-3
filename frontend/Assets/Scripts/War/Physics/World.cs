using System.Collections.Generic;
using Geometry;
using UnityEngine;
using War.Objects;
using Object = War.Objects.Object;


namespace War.Physics {

    public class World {

        public float Gravity;
        public float WaterLevel;
        public Land Land;
        public Tiles Tiles;
        private LinkedList<Object> _objects;

        private const float Precision = 0.1f;


        public World (Texture2D tex, SpriteRenderer renderer) {
            Gravity = -0.5f;
            WaterLevel = 0;
            Tiles = new Tiles();

            Land = new Land(
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
            _objects = new LinkedList<Object>();
        }


        public void Update (TurnData td) {
            //Debug.Log(objects.Count);

            if (Core.BF.State.Timer % 500 == 0 && td != null && td.MB) {
                AddObject(new Worm(), td.XY);
            }

            /*if (td != null) {
                if (td.mb) {
                    Core.bf.worm.position = td.xy;
                    Core.bf.worm.velocity = XY.left;// 5 * new XY(RNG.Float() - RNG.Float(), RNG.Float() - RNG.Float());
                }
            }*/

            foreach (var o in _objects) o.Update();
            foreach (var o in _objects) {
                o.Movement = 1;
                o.Excluded.Clear();
                o.ExcludeObjects();
            }

            for (int i = 0, iter = 5; i < iter; i++) {
                foreach (var o in _objects) {
                    if (o.Velocity.Length * o.Movement <= Precision) continue;

                    var c = o.NextCollision();
                    Object o2 = c == null || c.Collider2 == null ? null : c.Collider2.Obj;

                    if (c == null) {
                        // no collision
                        o.Position += o.Movement * (1 - Precision) * o.Velocity;
                        o.Movement = 0;
                    } else if (c.Collider2 == null) {
                        // collided with land
                        o.Position += c.Offset.WithLengthReduced(Precision);
                        o.Movement -= Mathf.Sqrt(c.Offset.SqrLength / o.Velocity.SqrLength);
                        o.Velocity = Geom.Bounce(
                            o.Velocity,
                            c.Normal,
                            Mathf.Sqrt(c.Collider1.TangentialBounce * Land.TangentialBounce),
                            Mathf.Sqrt(c.Collider1.NormalBounce * Land.NormalBounce)
                        );
                        o.OnCollision(c);
                    } else if (i > iter - 3 || o.SuperMass < o2.SuperMass) {
                        // force hard collision as if second object was land
                        // as long as magic number equals to 3, the eternal balance in the world will remain
                        o.Position += c.Offset.WithLengthReduced(Precision);
                        o.Movement -= Mathf.Sqrt(c.Offset.SqrLength / o.Velocity.SqrLength);
                        o.Velocity = Geom.Bounce(
                            o.Velocity,
                            c.Normal,
                            Mathf.Sqrt(c.Collider1.TangentialBounce * c.Collider2.TangentialBounce),
                            Mathf.Sqrt(c.Collider1.NormalBounce * c.Collider2.NormalBounce)
                        );
                        o.OnCollision(c);
                        o2.OnCollision(-c);
                    } else if (o2.SuperMass < o.SuperMass) {
                        // treat first object as it has infinite mass
                        o.Position += c.Offset.WithLengthReduced(Precision);
                        o.Movement -= Mathf.Sqrt(c.Offset.SqrLength / o.Velocity.SqrLength);
                        o.Velocity = Geom.Bounce(
                            o.Velocity,
                            c.Normal,
                            Mathf.Sqrt(c.Collider1.TangentialBounce * c.Collider2.TangentialBounce),
                            Mathf.Sqrt(c.Collider1.NormalBounce * c.Collider2.NormalBounce)
                        );
                        o.OnCollision(c);
                        o2.OnCollision(-c);
                    } else {
                        o.Position += c.Offset.WithLengthReduced(Precision);
                        o.Movement -= Mathf.Sqrt(c.Offset.SqrLength / o.Velocity.SqrLength);

                        // найти скалярное произведение нормали столкновения и скорости второго объекта
                        // так мы узнаем мешает ли он движению первого
                        float temp = XY.Dot(c.Normal, c.Collider2.Obj.Velocity);
                        if (temp >= 0 || c.Collider2.Obj.Movement * c.Collider2.Obj.Velocity.Length <= Precision) {
                            // collision
                            XY velocity = (o.Mass * o.Velocity + o2.Mass * o2.Velocity) / (o.Mass + o2.Mass);
                            XY v1 = o.Velocity - velocity;
                            XY v2 = o2.Velocity - velocity;
                            float tangBounce = Mathf.Sqrt(c.Collider1.TangentialBounce * c.Collider2.TangentialBounce);
                            float normBounce = Mathf.Sqrt(c.Collider1.NormalBounce * c.Collider2.NormalBounce);
                            o.Velocity = velocity + Geom.Bounce(v1, c.Normal, tangBounce, normBounce);
                            o2.Velocity = velocity + Geom.Bounce(v2, c.Normal, tangBounce, normBounce);
                        }

                        o.OnCollision(c);
                        o2.OnCollision(-c);
                    }
                    if (o.Position.Y < WaterLevel) o.Remove();
                }
            }

            // handle stuck objects:
            foreach (var o in _objects) {
                o.UpdateSpritePosition();
                o.Velocity *= 1 - o.Movement;
            }

            // clear all NullObjects WITHOUT INVALIDATING THEIR NODES!
            for (var node = _objects.First; node != null;) {
                if (node.Value is NullObject) {
                    var next = node.Next;
                    _objects.Remove(node);
                    node = next;
                } else node = node.Next;
            }

            //var list = new LinkedList<Object>(objects.Where<Object>(o => !(o is NullObject)));
            //objects.Clear();
            //objects = list;
        }


        public void AddObject (Object o, XY position, XY velocity = default(XY)) {
            o.Node = _objects.AddLast(o);
            o.Position = position;
            o.Velocity = velocity;
            o.OnAdd();
            o.UpdateSpritePosition();
        }

    }

}
