using Battle.Physics.Collisions;
using Battle.State;
using Core;
using DataTransfer.Data;
using Geometry;


namespace Battle.Objects.Controllers {

    public class WormControllerWalk : Controller {

        private readonly ActiveWormWrapper _activeWorm = The.ActiveWormWrapper;


        public override void OnAdd () {
            Object.Velocity = XY.Zero;
        }


        protected override void DoUpdate (TurnData td) {
            var worm = (Worm) Object;

            if (td != null) worm.LookAt(td.XY);

            var collision =
                new Ray(worm.Tail.Center, new CircleCollider(XY.Zero, Worm.HeadRadius))
                    .Cast(new XY(0f, -Worm.MaxDescend));

            // will fall?
            if (collision == null) {
                Object.Controller = new WormControllerJump();
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
                return;
            }

            if (td.A ^ td.D) worm.FacesRight = td.D;

            // jump
            if (td.S || td.W) {
                if (td.S) {
                    Object.Velocity = new XY(0f, Worm.HighJumpSpeed);
                    if (td.A ^ td.D) Object.Velocity.Rotate(td.A ? Worm.HighJumpAngle : -Worm.HighJumpAngle);
                } else {
                    Object.Velocity =
                        new XY(0f, Worm.JumpSpeed).Rotated(worm.FacesRight ? -Worm.JumpAngle : Worm.JumpAngle);
                }
                Object.Controller = new WormControllerJump();
                return;
            }

            float xOffset = 0f;
            if (td.A ^ td.D) {
                // try to walk
                xOffset = td.D ? Worm.WalkSpeed : -Worm.WalkSpeed;
                var rayOrigin = worm.Tail.Center + new XY(0f, Worm.MaxClimb);
                var rayDirection = new XY(
                    xOffset + (td.D ? World.Precision : -World.Precision),
                    0f
                );
                if (new Ray(rayOrigin, new CircleCollider(XY.Zero, Worm.HeadRadius)).Cast(rayDirection) != null) {
                    xOffset = 0f;
                }
            }

            // walk
            if (xOffset != 0f) {
                var rayOrigin = worm.Tail.Center + new XY(xOffset, Worm.MaxClimb);
                var rayDirection = new XY(0f, -Worm.MaxClimb - Worm.MaxDescend);
                var ray = new Ray(rayOrigin, new CircleCollider(XY.Zero, Worm.HeadRadius));
                var coll = ray.Cast(rayDirection);
                bool fall = coll == null;
                float yOffset = fall ? rayDirection.Y : coll.Offset.Y;
                rayDirection = new XY(0f, Worm.BodyHeight + yOffset);
                if (rayDirection.Y <= 0 || ray.Cast(rayDirection) == null) {
                    // move the worm
                    if (fall) {
                        Object.Position += new XY(xOffset, 0f);
                        Object.Controller = new WormControllerJump();
                    } else if (World.Precision < -yOffset) {
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
