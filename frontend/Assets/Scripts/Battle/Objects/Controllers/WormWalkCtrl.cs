using Battle.State;
using Collisions;
using Core;
using DataTransfer.Data;
using Geometry;


namespace Battle.Objects.Controllers {

    public class WormWalkCtrl : Controller {

        private readonly ActiveWorm _activeWorm = The.ActiveWorm;


        public override void OnAdd () {
            Object.Velocity = XY.Zero;
            Object.Immobile = true;
            ((Worm) Object).NewWormGO.Stand ();
        }


        public override void OnRemove () {
            Object.Immobile = false;
        }


        // todo: подумать где нужно заменить Precision на 0
        protected override void DoUpdate (TurnData td) {
            var worm = (Worm) Object;

//            if (td != null) worm.LookAt(td.XY);
            worm.Name = "walk";

            var collision = new Ray(worm.Tail.Center, new CircleCollider(XY.Zero, Worm.HeadRadius)) {Object = Object}
                .Cast(new XY(0f, -Worm.MaxDescend));

            // will fall?
            if (collision == null) {
                Object.Controller = new WormJumpCtrl();
                return;
            }

            // can move?
            if (td == null || !_activeWorm.Is(worm) || !_activeWorm.CanMove) {
                if (-collision.Offset.Y < World.Precision) {
                    collision.Offset.Y = 0;
                } else {
                    collision.Offset.Y += World.Precision;
                }
                Object.Position += collision.Offset;
                worm.NewWormGO.SetWalking (false);
                return;
            }

            if (td.A ^ td.D) worm.FacesRight = td.D;

            // jump
            if (td.S || td.W) {
                XY vj;
                if (td.S) {
                    vj = new XY(0f, Worm.HighJumpSpeed);
                    if (td.A ^ td.D) vj.Rotate(td.A ? Worm.HighJumpAngle : -Worm.HighJumpAngle);
                }
                else {
                    vj = new XY(0f, Worm.JumpSpeed).Rotated(worm.FacesRight ? -Worm.JumpAngle : Worm.JumpAngle);
                }
                Object.Controller = new WormBeforeJumpCtrl(vj);
                return;
            }

            worm.NewWormGO.SetWalking (td.A ^ td.D);
            
            float xOffset = 0f;
            if (td.A ^ td.D) {
                // try to walk
                xOffset = td.D ? Worm.WalkSpeed : -Worm.WalkSpeed;
                var rayOrigin = worm.Tail.Center + new XY (0f, Worm.MaxClimb);
                var rayDirection = new XY (
                    xOffset + (td.D ? World.Precision : -World.Precision),
                    0f
                );
                if (
                    new Ray (rayOrigin, new CircleCollider (XY.Zero, Worm.HeadRadius)) {Object = Object}.
                    Cast (rayDirection) != null
                ) {
                    xOffset = 0f;
                }
            }

            // walk
            if (xOffset != 0f) {
                var rayOrigin = worm.Tail.Center + new XY(xOffset, Worm.MaxClimb);
                var rayDirection = new XY(0f, -Worm.MaxClimb - Worm.MaxDescend);
                var ray = new Ray(rayOrigin, new CircleCollider(XY.Zero, Worm.HeadRadius)){Object = Object};
                var coll = ray.Cast(rayDirection);
                bool fall = coll == null;
                float yOffset = fall ? rayDirection.Y : coll.Offset.Y;
                rayDirection = new XY(0f, Worm.BodyHeight + yOffset);
                if (rayDirection.Y <= 0 || ray.Cast(rayDirection) == null) {
                    // move the worm
                    if (fall) {
                        Object.Position += new XY(xOffset, 0f);
                        Object.Controller = new WormJumpCtrl();
                    }
                    else if (World.Precision < -yOffset) {
                        Object.Position += new XY(xOffset, Worm.MaxClimb + yOffset + World.Precision);
                    } // else: cliff too high, cannot climb
                    return;
                }
                // head hit ceiling, dont move
            }

            // stand still
            if (-collision.Offset.Y < World.Precision) {
                collision.Offset.Y = 0;
            } else {
                collision.Offset.Y += World.Precision;
            }
            Object.Position += collision.Offset;
        }

    }

}
