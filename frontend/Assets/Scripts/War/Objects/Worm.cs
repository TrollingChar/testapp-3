using System;
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

        public const float HeadRadius = 8f;
        public const float BodyHeight = 8f;

        public const float WalkSpeed = 1f;
        public const float JumpSpeed = 6f;
        public const float HighJumpSpeed = 9f;

        public const float MaxClimb = 5f;
        public const float MaxDescend = 5f;

        [Obsolete] // TODO: replace this with properties Text and Color - get rid of null exceptions
        public WormGO SpriteExtension;
        
        public Team Team { get; set; }

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
        private int _hp;

        public int HP {
            get { return 60; }
            set { _hp = value; }
        } // TODO: convert to full property


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


        public virtual void OnAddToTeam (Team team) {
            SpriteExtension.Color = team.Color;
        }


        protected override bool PassableFor (Object o) {
            return this == o;
        }

    }

}
