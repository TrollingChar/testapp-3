using Geometry;
using UnityEngine;
using Utils;
using War.Objects.CollisionHandlers;
using War.Objects.Controllers;
using War.Objects.GameObjects;
using War.Physics.Collisions;
using War.Teams;
using Collider = War.Physics.Collisions.Collider;
using Collision = War.Physics.Collisions.Collision;
using UnObject = UnityEngine.Object;


namespace War.Objects {

    public class Worm : Object {

        public const float HeadRadius = 5f;
        public const float BodyHeight = 5f;

        public const float WalkSpeed = 1f;
        public const float JumpSpeed = 5f;
        public const float HighJumpSpeed = 7f;

        public const float MaxClimb = 5f;
        public const float MaxDescend = 5f;

        public WormGO SpriteExtension;

        public CircleCollider Head { get; private set; }
        public CircleCollider Tail { get; private set; }

        private bool _facesRight;

        public bool FacesRight {
            get { return _facesRight; }
            set { _facesRight = value; }
        }

        public bool FacesLeft {
            get { return !_facesRight; }
            set { _facesRight = !value; }
        }

        public Worm () : base(60, 1) {}


        public override void OnAdd () {
            Sprite = UnObject.Instantiate(Assets.Assets.Worm);
            SpriteExtension = Sprite.GetComponent<WormGO>();
            SpriteExtension.Text = "?";//WormsNames.Random();
            SpriteExtension.Text = "Кек";
            SpriteExtension.Color = RNG.Pick(TeamColors.Colors);

            AddCollider(Head = new CircleCollider(new XY(0f, BodyHeight * 0.5f), HeadRadius));
            AddCollider(Tail = new CircleCollider(new XY(0f, BodyHeight * -0.5f), HeadRadius));
            //AddCollider(new BoxCollider(-5, 5, -2.5f, 2.5f));

            Controller = new WormControllerJump();
        }


        protected override bool PassableFor (Object o) {
            return this == o;
        }

    }

}
