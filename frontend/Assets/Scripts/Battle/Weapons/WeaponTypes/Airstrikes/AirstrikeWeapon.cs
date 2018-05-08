using Attributes;
using Core;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.Airstrikes {

    [Weapon(WeaponId.Airstrike)]
    public class AirstrikeWeapon : StandardWeapon {

        private GameObject _crosshair;


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
            _crosshair = UnityEngine.Object.Instantiate(The.BattleAssets.PointCrosshair);
        }


        protected override void OnShoot () {
            UseAmmo();
            // todo: launch airplane/helicopter/ufo that will drop bombs to target location
        }


        protected override void OnUpdate () {
            _crosshair.transform.localPosition = new Vector3(TurnData.XY.X, TurnData.XY.Y);
        }


        protected override void OnUnequip () {
            UnityEngine.Object.Destroy(_crosshair);
        }

    }

}
