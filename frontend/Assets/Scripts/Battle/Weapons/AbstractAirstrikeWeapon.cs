using Battle.Objects.Projectiles;
using Battle.Weapons.Crosshairs;
using Core;
using Geometry;
using UnityEngine;
using Utils.Random;


namespace Battle.Weapons {

    public abstract class AbstractAirstrikeWeapon : StandardWeapon {

        private const float Dist = 30f;
        private PointCrosshair _crosshair;
        private float _leftX = float.MaxValue;
        private float _rightX = float.MinValue;
        
        private readonly World _world = The.World;
        private PointCrosshair.Direction _direction;


        protected PointCrosshair.Direction Direction {
            get { return _direction; }
            private set { _direction = _crosshair.Type = value; }
        }


        protected override void OnEquip () {
            _crosshair = UnityEngine.Object.Instantiate(The.BattleAssets.PointCrosshair).GetComponent<PointCrosshair>();
            Direction = RNG.Bool() ? PointCrosshair.Direction.Left : PointCrosshair.Direction.Right;
        }


        protected override void OnUpdate () {
            if (Direction == PointCrosshair.Direction.Right) {
                if (TurnData.XY.X < _leftX) {
                    Direction = PointCrosshair.Direction.Left;
                    _rightX = TurnData.XY.X + Dist;
                }
                else {
                    _leftX = Mathf.Max(_leftX, TurnData.XY.X - Dist);
                }
            }
            else {
                if (TurnData.XY.X > _rightX) {
                    Direction = PointCrosshair.Direction.Right;
                    _leftX = TurnData.XY.X - Dist;
                }
                else {
                    _rightX = Mathf.Min(_rightX, TurnData.XY.X + Dist);
                }
            }
            var target = Target;
            _crosshair.transform.localPosition = new Vector3(target.X, target.Y);
        }


        protected XY Target {
            get {
                return new XY(TurnData.XY.X, Mathf.Min(_world.Height, TurnData.XY.Y));
            }
        }


        protected override void OnUnequip () {
            UnityEngine.Object.Destroy(_crosshair.gameObject);
        }

        

    }

}
