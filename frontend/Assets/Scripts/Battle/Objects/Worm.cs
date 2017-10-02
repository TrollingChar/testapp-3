using Assets;
using Battle.Objects.Controllers;
using Battle.Objects.GameObjects;
using Battle.Physics.Collisions;
using Battle.Teams;
using Geometry;
using UnityEngine;
using Utils.Random;
using Utils.Singleton;
using UnObject = UnityEngine.Object;


namespace Battle.Objects {

    public class Worm : Object {

//        [Inject] private AssetContainer _assets;

        public const float HeadRadius = 8f;
        public const float BodyHeight = 8f;

        public const float WalkSpeed = 1f;
        public const float JumpSpeed = 6f;
        public const float HighJumpSpeed = 9f;

        public const float MaxClimb = 5f;
        public const float MaxDescend = 5f;
        private bool _arrowVisible;
        private Color _color;

        private bool _facesRight;
        private int _hp;
        private string _name;

        private WormGO _wormGO;


        public Worm (string name = "?", int hp = 60) : base(60, 1) {
            Name = name;
            HP = hp;
            FacesRight = RNG.Bool();
        }


        public Team Team { get; set; }

        public CircleCollider Head { get; private set; }
        public CircleCollider Tail { get; private set; }

        public bool FacesRight {
            get { return _facesRight; }
            set {
                _facesRight = value;
                if (_wormGO != null) _wormGO.FacesRight = _facesRight;
            }
        }

        public bool FacesLeft {
            get { return !_facesRight; }
            set {
                _facesRight = !value;
                if (_wormGO != null) _wormGO.FacesRight = _facesRight;
            }
        }

        public int HP {
            get { return _hp; }
            set {
                _hp = value;
                if (_wormGO != null) _wormGO.HP = value;
            }
        }

        public string Name {
            get { return _name; }
            set {
                _name = value;
                if (_wormGO != null) _wormGO.Name = value;
            }
        }

        public Color Color {
            get { return _color; }
            set {
                _color = value;
                if (_wormGO != null) _wormGO.Color = value;
            }
        }


        public bool ArrowVisible {
            get { return _arrowVisible; }
            set {
                _arrowVisible = value;
                if (_wormGO != null) _wormGO.ArrowVisible = value;
            }
        }


        public override void OnAdd () {
            var obj = UnObject.Instantiate(The<BattleAssets>.Get().Worm, GameObject.transform);
            _wormGO = obj.GetComponent<WormGO>();
            _wormGO.OnAdd(this);

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
            if (_wormGO == null) return;
            _wormGO.Look(Mathf.Rad2Deg * XY.DirectionAngle(Head.Center, target));
        }

    }

}
