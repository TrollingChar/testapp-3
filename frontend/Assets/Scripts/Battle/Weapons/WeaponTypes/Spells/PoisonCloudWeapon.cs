using Attributes;
using Battle.Objects.Explosives;
using Core;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.PoisonCloud)]
    public class PoisonCloudWeapon : StandardWeapon {

        private GameObject _crosshair;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.PoisonCloud,
                    The.WeaponIcons.PoisonCloud,
                    "ядовитое облако"
                );
            }
        }


        protected override void OnEquip () {
            _crosshair = UnityEngine.Object.Instantiate(The.BattleAssets.PointCrosshair);
        }


        protected override void OnShoot () {
            UseAmmo();
            new ExplosivePoison().Detonate(TurnData.XY);
        }


        protected override void OnUpdate () {
            _crosshair.transform.localPosition = new Vector3(TurnData.XY.X, TurnData.XY.Y);
        }


        protected override void OnUnequip () {
            UnityEngine.Object.Destroy(_crosshair);
        }

    }

}
