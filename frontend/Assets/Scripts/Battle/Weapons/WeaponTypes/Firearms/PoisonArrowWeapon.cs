using System;
using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.PoisonArrow)]
    public class PoisonArrowWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.PoisonArrow,
                    The<WeaponIcons>.Get().PoisonArrow
                );
            }
        }


        protected override void OnEquip () {
            Attacks = 2;
            // todo: what if player has only 1 arrow
            CrossHair = new LineCrosshair();
        }


        protected override void OnShoot () {}

    }

}
