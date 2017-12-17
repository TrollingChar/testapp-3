using Battle.Objects.Controllers;
using Battle.Objects.GameObjects;
using Battle.Teams;
using Battle.Weapons;
using Collisions;
using Core;
using Geometry;
using UnityEngine;
using UnityEngine.UI;
using Utils.Random;
using BoxCollider = Collisions.BoxCollider;
using UnObject = UnityEngine.Object;
using Time = Core.Time; 


namespace Battle.Objects {

    public class Worm : Object {

//        [Inject] private AssetContainer _assets;

        public const float HeadRadius = 8f;
        public const float BodyHeight = 8f;

        public const float WalkSpeed = 1f;
        public const float JumpSpeed = 6f;
        public const float HighJumpSpeed = 9f;
        public const float JumpAngle = 0.5f;
        public const float HighJumpAngle = 0.1f;

        public const float MaxClimb = 5f;
        public const float MaxDescend = 5f;

        private bool _arrowVisible;
        private Color _color;
        private bool _facesRight;
        private int _hp;
        private string _name;

        private WormGO _wormGO;
        private SpriteRenderer _arrow;
        private Text _nameField;
        private Text _hpField;

        private GameObject _canvas;
        private Weapon _weapon;


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
                _hp = value < 0 ? 0 : value;
                if (_hpField != null) _hpField.text = _hp.ToString();
            }
        }

        public string Name {
            get { return _name; }
            set {
                _name = value;
                if (_nameField != null) _nameField.text = value;
            }
        }

        public Color Color {
            get { return _color; }
            set {
                _color = value;
                if (_hpField != null) _hpField.color = value;
                if (_nameField != null) _nameField.color = value;
                if (_arrow != null) _arrow.color = value;
            }
        }


        public bool ArrowVisible {
            get { return _arrowVisible; }
            set {
                _arrowVisible = value;
                if (_arrow != null) _arrow.enabled = value;
            }
        }

        public Weapon Weapon {
            get { return _weapon; }
            set { SwapComponent(ref _weapon, value); }
        }


        public override void OnAdd () {
            InitGraphics();

            Name = "Кек";

            AddCollider(Head = new CircleCollider(new XY(0f, BodyHeight * 0.5f), HeadRadius));
            AddCollider(Tail = new CircleCollider(new XY(0f, BodyHeight * -0.5f), HeadRadius));
            AddCollider(new BoxCollider(-HeadRadius, HeadRadius, BodyHeight * -0.5f, BodyHeight * 0.5f));

            Controller = new WormControllerJump();
        }


        private void InitGraphics () {
            var assets = The.BattleAssets;
            var transform = GameObject.transform;

            _canvas = UnObject.Instantiate(assets.TopCanvas, transform, false);
            _canvas.transform.localPosition += new Vector3(0, 20, 0);
            _canvas.transform.localScale = new Vector3(0.7f, 0.7f, 1f);

            _nameField = UnObject.Instantiate(assets.Text, _canvas.transform, false).GetComponent<Text>();
            _hpField = UnObject.Instantiate(assets.Text, _canvas.transform, false).GetComponent<Text>();

            _arrow = UnObject.Instantiate(assets.Arrow, transform, false).GetComponentInChildren<SpriteRenderer>();

            var obj = UnObject.Instantiate(assets.Worm, transform, false);
            _wormGO = obj.GetComponent<WormGO>();
            _wormGO.OnAdd(this);

            HP = HP;
            Name = Name;
            ArrowVisible = false;
        }


        public virtual void OnAddToTeam (Team team) {
            Color = team.Color;
            // todo remove temp code
//            Color = RNG.Pick(TeamColors.Colors);
        }


        protected override bool PassableFor (Object o) {
            return this == o;
        }


        public void LookAt (XY target) {
            if (_wormGO == null) return;
            _wormGO.Look(Mathf.Rad2Deg * XY.DirectionAngle(Head.Center, target));
        }


        public override void GetDamage (int damage) {
            HP -= damage;
            OnDamage();
        }


        private void OnDamage () {
            var activeWorm = The.ActiveWorm;
            if (!activeWorm.Is(this)) return;
            if (Weapon != null) {
                Weapon.InitRetreat(new Time());
                Weapon.Unequip();
            }
            activeWorm.Set(null);
            Weapon = null;
        }


        public override void ReceiveBlastWave (XY impulse) {
            Controller = new WormControllerFall();
            base.ReceiveBlastWave(impulse);
        }


        public bool CanLandThere {
            get {
                /*throw new NotImplementedException();*/
                return true;
            }
        }


        public void LandThere () {
            Controller = new WormControllerJump();
        }


        public override void OnRemove () {
            HP = 0;
            OnDamage();
        }

    }

}
