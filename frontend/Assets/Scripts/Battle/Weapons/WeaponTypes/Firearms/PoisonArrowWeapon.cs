using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Core;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.PoisonArrow)]
    public class PoisonArrowWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.PoisonArrow,
                    The.WeaponIcons.PoisonArrow
                );
            }
        }


        protected override void OnEquip () {
            Attacks = 2;
            // todo: what if player has only 1 arrow
//            CrossHair = new LineCrosshair();
        }


        protected override void OnShoot () {}

    }

}
