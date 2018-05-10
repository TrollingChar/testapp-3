using Attributes;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;
using Utils.Random;


namespace Battle.Weapons.WeaponTypes.Airstrikes {

    [Weapon(WeaponId.Airstrike)]
    public class AirstrikeWeapon : StandardWeapon {

        private const float Dist = 30f;
        private PointCrosshair _crosshair;
        private float _leftX = float.MaxValue;
        private float _rightX = float.MinValue;
        
        private PointCrosshair.Direction _direction;
        public PointCrosshair.Direction Direction {
            get { return _direction; }
            set { _direction = _crosshair.Type = value; }
        }


        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Airstrike,
                    The.WeaponIcons.Airstrike,
                    "бомбардировка"
                );
            }
        }


        protected override void OnEquip () {
            _crosshair = UnityEngine.Object.Instantiate(The.BattleAssets.PointCrosshair).GetComponent<PointCrosshair>();
            Direction = RNG.Bool() ? PointCrosshair.Direction.Left : PointCrosshair.Direction.Right;
        }


        protected override void OnShoot () {
            UseAmmo();
            // todo: launch airplane/helicopter/ufo that will drop bombs to target location
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
            _crosshair.transform.localPosition = new Vector3(TurnData.XY.X, TurnData.XY.Y);
        }


        protected override void OnUnequip () {
            UnityEngine.Object.Destroy(_crosshair.gameObject);
        }

    }

}
