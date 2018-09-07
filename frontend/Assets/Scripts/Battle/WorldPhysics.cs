using Core;
using DataTransfer.Data;
using Geometry;
using UnityEngine;
using Collision = Collisions.Collision;
using Object = Battle.Objects.Object;


namespace Battle {

    public partial class World {
        
        private void PhysicsTick (TurnData td) {
            foreach (var o in Objects) {
                o.Movement = 1;
                o.Excluded.Clear();
                o.ExcludeObjects();
            }

            for (int i = 0, iter = 5; i < iter; i++)
            for (var node = Objects.First; node != null; node = node.Next) {
                var o = node.Value;

                // слишком низкая скорость
                if (o.Velocity.Length * o.Movement <= Settings.PhysicsPrecision) continue;

                // находим коллизию
                var collision = o.NextCollision (o.Movement);
                if (collision != null) collision.ImprovePrecision();
                
                /*
                 * а вот теперь
                 *
                 * если коллизии нет, то пролететь и сбросить movement
                 * если столкнулись с землей, то объект должен отскочить
                 * если столкновение на 2 последних итерациях, то скорость второго объекта считаем 0
                 * если столкнулись 2 нетолкаемых объекта, то скорость считаем 0, но проверяем может ли второй объект двигаться
                 * если только один из объектов нетолкаемый, то нулем считаем только его скорость
                 * если столкновение обычное то отскок по формуле
                 */
                
                if (collision == null) {
                    o.Position += (o.Movement * o.Velocity).WithLengthReduced (Settings.PhysicsPrecision);
                    o.Movement = 0;
                }
                else if (collision.IsLandCollision) {
                    collision.DoMove ();
                    
                    var с = collision.Collider1;
                    o.Velocity = Geom.Bounce (
                        o.Velocity,
                        collision.Normal,
                        Mathf.Sqrt (с.TangentialBounce * Land.TangentialBounce),
                        Mathf.Sqrt (с.NormalBounce     * Land.NormalBounce)
                    );
                    o.OnCollision (collision);
                }
                else if (i >= iter - 2) {
                    collision.DoMove ();
                    collision.DoBounceForce ();
                }
                else {
                    collision.DoMove ();
                    collision.DoBounce ();
                }
                
                /*
                if (collision == null) {
                    o.Position += (o.Movement * o.Velocity).WithLengthReduced (Settings.PhysicsPrecision);
                    o.Movement = 0;
                }
                else if (collision.IsLandCollision) {
                    _Move (o, collision.Offset);
                    
                    var с = collision.Collider1;
                    o.Velocity = Geom.Bounce (
                        o.Velocity,
                        collision.Normal,
                        Mathf.Sqrt (с.TangentialBounce * Land.TangentialBounce),
                        Mathf.Sqrt (с.NormalBounce * Land.NormalBounce)
                    );
                    o.OnCollision (collision);
                }
                else if (i >= iter - 2) {
                    // столкновение с несдвигаемым объектом считая его скорость нулевой
                    _Move (o, collision.Offset);

                    // не проверяем может ли o2 двигать o или не может, а то прилипания появятся опять
                    // да и вообще он и не должен его двигать
                    var c1 = collision.Collider1;
                    var c2 = collision.Collider2;
                    o.Velocity = Geom.Bounce (
                        o.Velocity,
                        collision.Normal,
                        Mathf.Sqrt (c1.TangentialBounce * c2.TangentialBounce),
                        Mathf.Sqrt (c1.NormalBounce     * c2.NormalBounce)
                    );

                    o.OnCollision (collision);
                    o2.OnCollision (-collision);
                }
                else if (!o2.PushableFor (o)) {
                    // o не может двигать o2
                    _Move (o, collision.Offset);
                    
                    var c1 = collision.Collider1;
                    var c2 = collision.Collider2;
                    // так че разность скоростей считать чтоли
                    var dv = o.Velocity - o2.Velocity;
                    o.Velocity = Geom.Bounce (
                        dv,
                        collision.Normal,
                        Mathf.Sqrt (c1.TangentialBounce * c2.TangentialBounce),
                        Mathf.Sqrt (c1.NormalBounce     * c2.NormalBounce)
                    ) + o2.Velocity;

                    o.OnCollision (collision);
                    o2.OnCollision (-collision);
                }
                else if (!o.PushableFor (o2)) {
                    // скорость нашего объекта не меняется, но второй отскакивает
                    _Move (o, collision.Offset);

                    var c1 = collision.Collider1;
                    var c2 = collision.Collider2;
                    var dv = o2.Velocity - o.Velocity;
                    o2.Velocity = Geom.Bounce (
                        dv,
                        collision.Normal,
                        Mathf.Sqrt (c1.TangentialBounce * c2.TangentialBounce),
                        Mathf.Sqrt (c1.NormalBounce     * c2.NormalBounce)
                    ) + o.Velocity;
                    
                    o.OnCollision (collision);
                    o2.OnCollision (-collision);
                }
                else {
                    // обычное столкновение объектов с одинаковым порядком массы
                    _Move (o, collision.Offset);

                    // мешает ли второй объект движению первого, если нет то пусть сначала сдвинется он
                    if (
                        XY.Dot(collision.Normal, o2.Velocity) > 0 ||
                        o2.Movement * o2.Velocity.Length <= Settings.PhysicsPrecision
                    ) {
                        var invCollision = -collision;
                        bool owcc = o.WillCauseCollision(collision);
                        bool o2wcc = o2.WillCauseCollision(invCollision);

                        if (owcc || o2wcc) {
                            var velocity
                                = (o.Mass * o.Velocity + o2.Mass * o2.Velocity)
                                / (o.Mass + o2.Mass);
                            var v1 = o.Velocity - velocity;
                            var v2 = o2.Velocity - velocity;
                            var c1 = collision.Collider1;
                            var c2 = collision.Collider2;
                            float tangBounce = Mathf.Sqrt(c1.TangentialBounce * c2.TangentialBounce);
                            float normBounce = Mathf.Sqrt(c1.NormalBounce * c2.NormalBounce);
                            if (o2wcc) o.Velocity = velocity + Geom.Bounce(v1, collision.Normal, tangBounce, normBounce);
                            if (owcc) o2.Velocity = velocity + Geom.Bounce(v2, collision.Normal, tangBounce, normBounce);
                        }

                        o.OnCollision(collision);
                        o2.OnCollision(invCollision);
                    }
                }*/

                if (o.WillSink && o.Position.Y < WaterLevel) o.Despawn();
            }

            // уменьшить скорость объектов, не потративших очки движения, так как они застряли
            foreach (var o in Objects) {
                o.UpdateGameObjectPosition();
                o.Velocity *= 1 - o.Movement;
            }
        }

    }

}