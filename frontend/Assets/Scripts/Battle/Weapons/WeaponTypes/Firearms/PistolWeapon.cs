using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.Pistol)]
    public class PistolWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Pistol,
                    The<WeaponIcons>.Get().Pistol
                );
            }
        }


        protected override void OnEquip () {
            Shots = 5;
//            CrossHair = new LineCrosshair();
        }


        protected override void OnBeginAttack () {
            UseAmmo();
        }


        protected override void OnShoot () {}

    }

}
