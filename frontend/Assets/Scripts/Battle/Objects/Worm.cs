using System;
using Battle.Objects.Controllers;
using Battle.Objects.Effects;
using Battle.Objects.Explosives;
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
//        private int _hpot; // over time
        private int _poison;
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
                if (Team != null) {
                    if (_hp <= 0) Team.WormsAlive++;
                    if (value <= 0) Team.WormsAlive--;
                }
                _hp = value < 0 ? 0 : value;
                UpdateHpText();
            }
        }

        
        public int Poison {
            get { return _poison; }
            set {
                _poison = value;
                UpdateHpText();
            }
        }


        private void UpdateHpText () {
            if (_hpField == null) return;
            string text = HP.ToString();
            if (_poison > 0) text += " (-" + Poison + ")";
            _hpField.text = text;
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
            Explosive = new Explosive15();
        }


        private void InitGraphics () {
            var assets = The.BattleAssets;
            var transform = GameObject.transform;

            _canvas = UnityEngine.Object.Instantiate(assets.TopCanvas, transform, false);
            _canvas.transform.localPosition += new Vector3(0, 20, 0);
            _canvas.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            _canvas.GetComponent<Canvas>().sortingLayerName = "TextBack";

            _nameField = UnityEngine.Object.Instantiate(assets.Text, _canvas.transform, false).GetComponent<Text>();
            _hpField = UnityEngine.Object.Instantiate(assets.Text, _canvas.transform, false).GetComponent<Text>();

            _arrow = UnityEngine.Object.Instantiate(assets.Arrow, transform, false).GetComponentInChildren<SpriteRenderer>();

            var obj = UnityEngine.Object.Instantiate(assets.Worm, transform, false);
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


        public override void TakeDamage (int damage) {
            if (damage <= 0) {
                Debug.LogWarning("damage <= 0");
                return;
            }
            HP -= damage;
            if (The.ActiveWorm.Is(this)) The.BattleScene.EndTurn();
            
            float effectTime = 1f + 1f * damage / (damage + 40f); 
            Spawn(
                new Label(damage.ToString(), _color, effectTime),
                Position,
//                Velocity +
                XY.FromPolar(4 + RNG.Float() * 4, (RNG.Float() - RNG.Float()) * 0.5f).Rotated90CCW()
            );
        }


        public override void AddPoison (int dpr, bool additive) {
            if (dpr <= 0) {
                Debug.LogWarning("poison <= 0");
                return;
            }
            if (additive) {
                Poison += dpr;
            } else if (Poison < dpr) {
                Poison = dpr;
            }
        }


        public override void CurePoison (int dpr) {
            if (dpr <= 0) {
                Debug.LogWarning("cure poison <= 0");
                return;
            }
            if (Poison > dpr) {
                Poison -= dpr;
            } else {
                Poison = 0;
            }
        }


        public override void CureAllPoison () {
            Poison = 0;
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
            if (The.ActiveWorm.Is(this)) The.BattleScene.EndTurn();
        }

    }

}
