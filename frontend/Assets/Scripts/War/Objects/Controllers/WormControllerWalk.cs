using Geometry;
using War.Physics;
using War.Physics.Collisions;


namespace War.Objects.Controllers {

    public class WormControllerWalk : Controller {

        // TODO: replace magic numbers with constants


        public override void OnAdd () {
            Object.Velocity = XY.Zero;
        }


        public override void Update (TurnData td) {
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
            if (td == null || worm != Core.BF.State.Worm || Core.BF.State.WormFrozen) {
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
                    if (td.A ^ td.D) Object.Velocity.Rotate(td.A ? 0.1f : -0.1f);
                } else {
                    Object.Velocity = new XY(0f, Worm.JumpSpeed).Rotated(worm.FacesRight ? -0.5f : 0.5f);
                }
                Object.Controller = new WormControllerJump();
                return;
            }

            float xOffset = 0f;
            if (td.A ^ td.D) {
                // try to walk
                xOffset = td.D ? Worm.WalkSpeed : -Worm.WalkSpeed;
                XY rayOrigin = worm.Tail.Center + new XY(0f, Worm.MaxClimb);
                XY rayDirection = new XY(
                    xOffset + (td.D ? World.Precision : -World.Precision),
                    0f
                );
                if (new Ray(rayOrigin, new CircleCollider(XY.Zero, Worm.HeadRadius)).Cast(rayDirection) != null) {
                    xOffset = 0f;
                }
            }

            // walk
            if (xOffset != 0f) {
                XY rayOrigin = worm.Tail.Center + new XY(xOffset, Worm.MaxClimb);
                XY rayDirection = new XY(0f, -Worm.MaxClimb - Worm.MaxDescend);
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
