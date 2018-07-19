using Attributes;
using Core;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.Erosion)]
    public class ErosionWeapon : StandardWeapon {

        private GameObject _crosshair;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Erosion,
                    The.WeaponIcons.Erosion,
                    "эрозия"
                );
            }
        }


        protected override void OnEquip () {
            _crosshair = UnityEngine.Object.Instantiate(The.BattleAssets.PointCrosshair);
        }


        protected override void OnShoot () {
            UseAmmo();
            The.World.DestroyTerrain(TurnData.XY, 50f);
        }


        protected override void OnUpdate () {
            _crosshair.transform.localPosition = new Vector3(TurnData.XY.X, TurnData.XY.Y);
        }


        protected override void OnUnequip () {
            UnityEngine.Object.Destroy(_crosshair);
        }

    }

}
