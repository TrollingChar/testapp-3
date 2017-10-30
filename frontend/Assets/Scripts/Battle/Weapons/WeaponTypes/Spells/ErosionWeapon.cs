using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.Erosion)]
    public class ErosionWeapon : StandardWeapon {

        private PointCrosshair _crosshair;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Erosion,
                    The<WeaponIcons>.Get().Erosion
                );
            }
        }


        protected override void OnEquip () {
//            _crosshair = UnityEngine.Object.Instantiate(
//                The<BattleAssets>.Get().PointCrosshair,
//                GameObject.transform,
//                false
//            ).GetComponent<PointCrosshair>();

            // todo: magic scroll or something
//            _sprite = UnityEngine.Object.Instantiate(
//                The<BattleAssets>.Get().GrenadeWeapon,
//                GameObject.transform,
//                false
//            );
        }


        protected override void OnShoot () {
            The<World>.Get().DestroyTerrain(TurnData.XY, 40f);
        }

    }

}
