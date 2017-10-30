using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Airstrikes {

    [Weapon(WeaponId.MineStrike)]
    public class MineStrikeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MineStrike,
                    The<WeaponIcons>.Get().MineStrike
                );
            }
        }


        protected override void OnEquip () {
//            CrossHair = new AirstrikeCrosshair();
        }


        protected override void OnShoot () {}

    }

}
