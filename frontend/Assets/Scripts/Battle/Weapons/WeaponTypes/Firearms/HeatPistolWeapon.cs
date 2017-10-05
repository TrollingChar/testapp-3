using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.HeatPistol)]
    public class HeatPistolWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.HeatPistol,
                    The<WeaponIcons>.Get().HeatPistol
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
