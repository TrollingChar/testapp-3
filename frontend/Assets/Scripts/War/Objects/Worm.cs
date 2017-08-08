using System;
using Assets;
using Geometry;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using War.Objects.CollisionHandlers;
using War.Objects.Controllers;
using War.Objects.GameObjects;
using War.Physics.Collisions;
using War.Teams;
using Zenject;
using Collider = War.Physics.Collisions.Collider;
using Collision = War.Physics.Collisions.Collision;
using UnObject = UnityEngine.Object;


namespace War.Objects {

    public class Worm : Object {

        [Inject] private AssetContainer _assets;
        
        public const float HeadRadius = 8f;
        public const float BodyHeight = 8f;

        public const float WalkSpeed = 1f;
        public const float JumpSpeed = 6f;
        public const float HighJumpSpeed = 9f;

        public const float MaxClimb = 5f;
        public const float MaxDescend = 5f;

        private WormGO _spriteExtension;

        public Team Team { get; set; }

        public CircleCollider Head { get; private set; }
        public CircleCollider Tail { get; private set; }

        private bool _facesRight;
        private int _hp;
        private string _name;
        private Color _color;
        private bool _arrowVisible;

        public bool FacesRight {
            get { return _facesRight; }
            set {
                _facesRight = value;
                if (_spriteExtension != null) _spriteExtension.FacesRight = _facesRight;
            }
        }

        public bool FacesLeft {
            get { return !_facesRight; }
            set {
                _facesRight = !value;
                if (_spriteExtension != null) _spriteExtension.FacesRight = _facesRight;
            }
        }

        public int HP {
            get { return _hp; }
            set {
                _hp = value;
                if (_spriteExtension != null) _spriteExtension.HP = value;
            }
        }

        public string Name {
            get { return _name; }
            set {
                _name = value;
                if (_spriteExtension != null) _spriteExtension.Name = value;
            }
        }

        public Color Color {
            get { return _color; }
            set {
                _color = value;
                if (_spriteExtension != null) _spriteExtension.Color = value;
            }
        }


        public bool ArrowVisible {
            get { return _arrowVisible; }
            set {
                _arrowVisible = value;
                if (_spriteExtension != null) _spriteExtension.ArrowVisible = value;
            }
        }


        public Worm (string name = "?", int hp = 60) : base(60, 1) {
            Name = name;
            HP = hp;
            FacesRight = RNG.Bool();
        }


        public override void OnAdd () {
            Sprite = UnObject.Instantiate(_assets.Worm);
            _spriteExtension = Sprite.GetComponent<WormGO>();
            _spriteExtension.OnAdd(this);

            Name = "Кек";

            AddCollider(Head = new CircleCollider(new XY(0f, BodyHeight * 0.5f), HeadRadius));
            AddCollider(Tail = new CircleCollider(new XY(0f, BodyHeight * -0.5f), HeadRadius));
            //AddCollider(new BoxCollider(-5, 5, -2.5f, 2.5f));

            Controller = new WormControllerJump();
        }


        public virtual void OnAddToTeam (Team team) {
            Color = team.Color;
        }


        protected override bool PassableFor (Object o) {
            return this == o;
        }


        public void LookAt (XY target) {
            if (_spriteExtension == null) return;
            _spriteExtension.Look(Mathf.Rad2Deg * XY.DirectionAngle(Head.Center, target));
        }

    }

}
